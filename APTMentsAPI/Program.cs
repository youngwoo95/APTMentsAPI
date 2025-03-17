
using APTMentsAPI.Repository;
using APTMentsAPI.Repository.TheHamBiz;
using APTMentsAPI.Services.Helpers;
using APTMentsAPI.Services.Logger;
using APTMentsAPI.Services.TheHamBizService;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace APTMentsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Kestrel ����
            builder.WebHost.UseKestrel((context, options) =>
            {
                options.Configure(context.Configuration.GetSection("Kestrel"));
                // Keep-Alive TimeOut 3�м��� keep-Alive Ÿ�Ӿƿ�: �Ϲ������� 2~5��, �ʹ� ª���� ������ ���� ������ �� �ְ�, �ʹ� ��� ���ҽ��� ����� �� ����.
                options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(3);
                // �ִ� ���� ���׷��̵� ���� ��: �Ϲ������� 1000 ~ 5000 ���̷� �����ϴ� ���� ����
                options.Limits.MaxConcurrentUpgradedConnections = 3000;
                options.Limits.MaxResponseBufferSize = null; // ���� ũ�� ���� ����
                options.ConfigureEndpointDefaults(endpointOptions =>
                {
                    // �������� ����: HTTP/1.1�� HTTP/2�� ��� �����ϴ� ���� ����.
                    // HTTP/2�� ���� ���� ȿ������ ������ ������ ������.
                    endpointOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                });
            });
            #endregion

            #region CORS
            var AllowCors = "AllowSpecificIP";
            string[]? CorsArr = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            if (CorsArr is [_, ..])
            {
                builder.Services.AddCors(options =>
                {
                    // ���� ��å: Ư�� Origin�� ���
                    options.AddPolicy(name: AllowCors, builder =>
                    {
                        builder.WithOrigins(CorsArr)
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });

                    // Ư�� ��Ʈ�ѷ��� ����� ��å: ��� Origin ���
                    options.AddPolicy("AllowAll", builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
                });
            }
            else
            {
                throw new InvalidOperationException("'Cors' is null or empty");
            }
            #endregion

            // Add services to the container.

            builder.Services.AddControllers();
           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DI
            builder.Services.AddTransient<ILoggerService, LoggerService>();

            builder.Services.AddTransient<IRequestAPI, RequestAPI>();

            builder.Services.AddTransient<ITheHamBizServices, TheHamBizServices>();
            builder.Services.AddTransient<ITheHamBizRepository, TheHamBizRepository>();
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

         

            var app = builder.Build();

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

            //app.UseHttpsRedirection();

            // ���� ��å ����
            app.UseCors(AllowCors);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
