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

            // ���񽺷� �����ص� ���������� ������.
            builder.Host.UseWindowsService();

            #region Kestrel ����
            builder.WebHost.UseKestrel((context, options) =>
            {
                //options.Configure(context.Configuration.GetSection("Kestrel"));
                options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1); // ������ ��û ����� �����ϴ� �� �ɸ��� �ִ� �ð��� �����Ѵ�.
                // Keep-Alive TimeOut 3�м��� keep-Alive Ÿ�Ӿƿ�: �Ϲ������� 2~5��, �ʹ� ª���� ������ ���� ������ �� �ְ�, �ʹ� ��� ���ҽ��� ����� �� ����.
                options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(3);

                /* Ŭ���̾�Ʈ ���� �� ���� */
                //options.Limits.MaxConcurrentConnections = 100; // �ִ� Ŭ���̾�Ʈ ���� ���� �����Ѵ�.
                // �ִ� ���� ���׷��̵� ���� ��: �Ϲ������� 1000 ~ 5000 ���̷� �����ϴ� ���� ����
                // �ִ� ����, ���׷��̵�� ���� ���� �����Ѵ�.
                options.Limits.MaxConcurrentUpgradedConnections = 3000;
                options.Limits.MaxResponseBufferSize = null; // ���� ũ�� ���� ����
                options.ConfigureEndpointDefaults(endpointOptions =>
                {
                    // �������� ����: HTTP/1.1�� HTTP/2�� ��� �����ϴ� ���� ����.
                    // HTTP/2�� ���� ���� ȿ������ ������ ������ ������.
                    endpointOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                });

                options.Listen(IPAddress.Any, 5255, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });
            });
            #endregion

            // ���޵� ����� �̵���� ����
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
            // CORS ����
            //var AllowCors = "AllowSpecificIP";
            //string[]? CorsArr = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            //if (CorsArr is [_, ..])
            //{

            //    builder.Services.AddCors(options =>
            //    {
            //        // ���� ��å: S-TEC API ȣ���
            //        options.AddPolicy(name: AllowCors, builder =>
            //        {
            //            builder.WithOrigins(CorsArr)
            //                   .AllowAnyMethod()
            //                   .AllowAnyHeader();
            //        });

            //        // ��ü ��� _ Ư����Ʈ�ѷ��� �������� (���Ժ��� API ȣ���)
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

            /* �ȵǼ� �߰��Ѻκ� */
            builder.Services
              .AddControllers(options =>
              {
                  // System.Text.Json ��� �����͸� ã�Ƽ�
                  var jsonFormatter = options.InputFormatters
                      .OfType<SystemTextJsonInputFormatter>()
                      .First();

                  // "application/json;charset=UTF-8" �� ��� �̵�� Ÿ������ �߰�
                  jsonFormatter.SupportedMediaTypes
                      .Add("application/json;charset=UTF-8");
              })
              .AddJsonOptions(opts =>
              {
                  // (����) JSON ����ȭ/������ȭ �ɼ�
                  // 2. ��ҹ��� ���� ���� ����
                  opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
              });

            // Swagger ���� �߰�
#if DEBUG
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();  // SwaggerResponse ��Ʈ����Ʈ ��� (�ɼ�)
                c.ExampleFilters(); // SwaggerResponse ��Ʈ����Ʈ ���
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "����Ʈ���� ���α׷�",
                    Description = "���Ժ��� API ����"
                });
            });
#endif

#if DEBUG
            // ���� ���͸� �����ϴ� ����� ���
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
                        // Ÿ�Ӿƿ� 1��
                        mySqlOptions.CommandTimeout(60);
                        mySqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); // ������ ������ ���� ����� ���� ���� ���� ���
                    }));

                builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));
            }
            else
                throw new InvalidOperationException("Connection string 'DefaultConnection' is null or empty.");

            #endregion

            #region SIGNAL R ���
            builder.Services.AddSignalR().AddHubOptions<BroadcastHub>(options =>
            {
                options.EnableDetailedErrors = false; // ��꿡�� ������ �߻��� ��, Ŭ���̾�Ʈ���� �ڼ��� ���� ������ �����Ѵ�. - �ΰ��� ������ ������ �� �����Ƿ� false�� ���� ����
                options.KeepAliveInterval = System.TimeSpan.FromSeconds(15); // ������ Ŭ���̾�Ʈ�� �ֱ������� ���� ���� (��Ʈ��Ʈ)
                options.HandshakeTimeout = System.TimeSpan.FromSeconds(15); // Ŭ���̾�Ʈ�� ���� �ڵ弼��ũ�� �Ϸ��� �� ���� ��ٸ��� �ִ�ð� �Ѿ�� ����ó��.
                options.ClientTimeoutInterval = System.TimeSpan.FromSeconds(30); // Ŭ���̾�Ʈ�κ��� ��Ʈ��Ʈ�� ���޾��� �� ������ 30�ʵ��� ��ٷ��ش�.
                //options.MaximumReceiveMessageSize = 32 * 1024; // 32KB ������ Ŭ���̾�Ʈ�κ��� ������ �� �ִ� �ִ� �޽��� ũ�⸦ ����Ʈ������ ����
                //options.StreamBufferCapacity = 10; // Ŭ���̾�Ʈ�� ���� �� ��Ʈ���� �� ���ۿ� ������ �� �ִ� �������� �ִ� ��
                // options.MaximumParallelInvocationsPerClient = 1; // �ϳ��� Ŭ���̾�Ʈ�� ���ÿ� ������ �� �ִ� ��� �޼��� ȣ���� �ִ� ������ ����. -> �������� ���� ���ϰ� ����⶧��
            });
            #endregion

            var app = builder.Build();

            // DB ���ٸ� ���̱׷��̼�
            /*
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AptContext>();

                // �����ͺ��̽� ������ �ȵȴٸ�(��, �����ͺ��̽��� ������)
                if (!context.Database.CanConnect())
                {
                    // �����ͺ��̽� ���� �� ���̱׷��̼� ����
                    context.Database.Migrate();
                }
            }
            */

            #region ������ ���Ͻ� ���� ���
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
            app.UseResponseCompression(); // ���� ���� �̵���� �߰�
            app.MapHub<BroadcastHub>("/ParkingHub");

            /* 
               MIME Ÿ�� �� ���� ��� ����
               �⺻ �������� �ʴ� MIME Ÿ�� �߰�.
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
                    /* ����� ���Ͽ� ���� Content-Encoding ��� ���� */
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

            // ���� ��å ����
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}