using APTMentsAPI.Repository;
using APTMentsAPI.Repository.TheHamBiz;
using APTMentsAPI.Services.FileService;
using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.IpSetting;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.Names;
using APTMentsAPI.Services.TheHamBizService;
using APTMentsAPI.SignalRHub;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Swashbuckle.AspNetCore.Filters;
using System.Data;
using System.Net;


namespace APTMentsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 서비스로 동작해도 문제없도록 설정함.
            builder.Host.UseWindowsService();

            #region Kestrel 서버
            builder.WebHost.UseKestrel((context, options) =>
            {
                //options.Configure(context.Configuration.GetSection("Kestrel"));
                options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1); // 서버가 요청 헤더를 수신하는 데 걸리는 최대 시간을 설정한다.
                // Keep-Alive TimeOut 3분설정 keep-Alive 타임아웃: 일반적으로 2~5분, 너무 짧으면 연결이 자주 끊어질 수 있고, 너무 길면 리소스가 낭비될 수 있음.
                options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(3);

                /* 클라이언트 연결 수 지정 */
                //options.Limits.MaxConcurrentConnections = 100; // 최대 클라이언트 연결 수를 지정한다.
                // 최대 동시 업그레이드 연결 수: 일반적으로 1000 ~ 5000 사이로 설정하는 것이 좋음
                // 최대 열린, 업그레이드된 연결 수를 설정한다.
                options.Limits.MaxConcurrentUpgradedConnections = 3000;
                options.Limits.MaxResponseBufferSize = null; // 응답 크기 제한 해제
                options.ConfigureEndpointDefaults(endpointOptions =>
                {
                    // 프로토콜 설정: HTTP/1.1과 HTTP/2를 모두 지원하는 것을 권장.
                    // HTTP/2는 성능 향상과 효율적인 데이터 전송을 제공함.
                    endpointOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                });

                options.Listen(IPAddress.Any, 5255, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });
            });
            #endregion

            // 전달된 헤더의 미들웨어 순서
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                //options.Providers.Add<CustomCompressionProvider>();
                options.MimeTypes =
                ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "image/svg+xml" });
            });

            #region CORS
            // CORS 설정
            //var AllowCors = "AllowSpecificIP";
            //string[]? CorsArr = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            //if (CorsArr is [_, ..])
            //{

            //    builder.Services.AddCors(options =>
            //    {
            //        // 전역 정책: S-TEC API 호출시
            //        options.AddPolicy(name: AllowCors, builder =>
            //        {
            //            builder.WithOrigins(CorsArr)
            //                   .AllowAnyMethod()
            //                   .AllowAnyHeader();
            //        });

            //        // 전체 허용 _ 특정컨트롤러에 따로지정 (더함비즈 API 호출시)
            //        options.AddPolicy("AllowAll", builder =>
            //        {
            //            builder.AllowAnyOrigin()
            //                   .AllowAnyMethod()
            //                   .AllowAnyHeader();
            //        });
            //    });
            //}
            //else
            //{
            //    throw new InvalidOperationException("'Cors' is null or empty");
            //}
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            #endregion

            // Add services to the container.

            //builder.Services.AddControllers();

            /* 안되서 추가한부분 */
            builder.Services
              .AddControllers(options =>
              {
                  // System.Text.Json 기반 포맷터를 찾아서
                  var jsonFormatter = options.InputFormatters
                      .OfType<SystemTextJsonInputFormatter>()
                      .First();

                  // "application/json;charset=UTF-8" 도 허용 미디어 타입으로 추가
                  jsonFormatter.SupportedMediaTypes
                      .Add("application/json;charset=UTF-8");
              })
              .AddJsonOptions(opts =>
              {
                  // (선택) JSON 직렬화/역직렬화 옵션
                  // 2. 대소문자 구분 없이 매핑
                  opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
              });

            // Swagger 설정 추가
#if DEBUG
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();  // SwaggerResponse 어트리뷰트 사용 (옵션)
                c.ExampleFilters(); // SwaggerResponse 어트리뷰트 사용
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "아파트관리 프로그램",
                    Description = "더함비즈 API 연동"
                });
            });
#endif

#if DEBUG
            // 예제 필터를 포함하는 어셈블리 등록
            builder.Services.AddSwaggerExamplesFromAssemblyOf<ViewListResponseExample>();
            builder.Services.AddSwaggerExamplesFromAssemblyOf<DetailViewResponseExample>();
            builder.Services.AddSwaggerExamplesFromAssemblyOf<LastViewListResponseExample>();
            builder.Services.AddSwaggerExamplesFromAssemblyOf<PatrolListResponseExample>();
#endif

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DI
            builder.Services.AddTransient<ILoggerService, LoggerService>();

            builder.Services.AddTransient<IRequestAPI, RequestAPI>();

            builder.Services.AddTransient<IFileService, FileService>();

            builder.Services.AddTransient<ITheHamBizServices, TheHamBizServices>();
            builder.Services.AddTransient<ITheHamBizRepository, TheHamBizRepository>();
            builder.Services.AddTransient<IAptNameService, AptNameService>();
            builder.Services.AddTransient<IIpSettingService, IpSettingService>();
            #endregion

            #region DB
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (!String.IsNullOrWhiteSpace(connectionString))
            {
                builder.Services.AddDbContext<AptContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.Parse("10.11.7-mariadb"),
                    mySqlOptions =>
                    {
                        // 타임아웃 1분
                        mySqlOptions.CommandTimeout(60);
                        mySqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); // 복잡한 쿼리의 성능 향상을 위한 쿼리 분할 사용
                    }));

                builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));
            }
            else
                throw new InvalidOperationException("Connection string 'DefaultConnection' is null or empty.");

            #endregion

            #region SIGNAL R 등록
            builder.Services.AddSignalR().AddHubOptions<BroadcastHub>(options =>
            {
                options.EnableDetailedErrors = false; // 허브에서 오류가 발생할 때, 클라이언트에게 자세한 오류 정보를 전송한다. - 민감한 정보를 전송할 수 있으므로 false로 보통 설정
                options.KeepAliveInterval = System.TimeSpan.FromSeconds(15); // 서버가 클라이언트로 주기적으로 핑을 보냄 (하트비트)
                options.HandshakeTimeout = System.TimeSpan.FromSeconds(15); // 클라이언트가 연결 핸드세이크를 완료할 때 까지 기다리는 최대시간 넘어가면 에러처리.
                options.ClientTimeoutInterval = System.TimeSpan.FromSeconds(30); // 클라이언트로부터 하트비트를 못받았을 때 서버가 30초동안 기다려준다.
                //options.MaximumReceiveMessageSize = 32 * 1024; // 32KB 서버가 클라이언트로부터 수신할 수 있는 최대 메시지 크기를 바이트단위로 설정
                //options.StreamBufferCapacity = 10; // 클라이언트와 서버 간 스트리밍 시 버퍼에 저장할 수 있는 아이템의 최대 수
                // options.MaximumParallelInvocationsPerClient = 1; // 하나의 클라이언트가 동시에 수행할 수 있는 허브 메서드 호출의 최대 개수를 제한. -> 많아지면 서버 부하가 생기기때문
            });
            #endregion

            var app = builder.Build();

            // DB 없다면 마이그레이션
            /*
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AptContext>();

                // 데이터베이스 연결이 안된다면(즉, 데이터베이스가 없으면)
                if (!context.Database.CanConnect())
                {
                    // 데이터베이스 생성 및 마이그레이션 적용
                    context.Database.Migrate();
                }
            }
            */

            #region 역방향 프록시 서버 사용
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            #endregion

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseResponseCompression(); // 응답 압축 미들웨어 추가
            app.MapHub<BroadcastHub>("/ParkingHub");

            /* 
               MIME 타입 및 압축 헤더 설정
               기본 제공되지 않는 MIME 타입 추가.
            */
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings =
                    {
                        [".wasm"] = "application/wasm",
                        [".gz"] = "application/octet-stream",
                        [".br"] = "application/octet-stream",
                        [".jpg"] = "image/jpg",
                        [".jpeg"] ="image/jpeg",
                        [".png"] = "image/png",
                        [".gif"] = "image/gif",
                        [".webp"] = "image/webp",
                        [".xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        [".pdf"] = "application/pdf"
                    }
                },
                OnPrepareResponse = ctx =>
                {
                    /* 압축된 파일에 대한 Content-Encoding 헤더 설정 */
                    if (ctx.File.Name.EndsWith(".gz"))
                    {
                        ctx.Context.Response.Headers["Content-Encoding"] = "gzip";
                    }
                    else if (ctx.File.Name.EndsWith(".br"))
                    {
                        ctx.Context.Response.Headers["Content-Encoding"] = "br";
                    }
                }
            });

            // 전역 정책 적용
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}