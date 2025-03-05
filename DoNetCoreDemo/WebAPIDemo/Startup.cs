using DoNetCoreDemo.Model;
using DoNetCoreDemo.Service;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPIDemo.Data;

namespace WebAPIDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            ////通过依赖关系注入注册的上下文
            ////指定生命周期（默认为Scoped）
            //1.不需要Mapping
            services.AddDbContext<ApplicationDbContext>(options =>
                  //Microsoft.EntityFrameworkCore.SqlServer
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")),
                  contextLifetime: ServiceLifetime.Scoped,
                  optionsLifetime: ServiceLifetime.Singleton
                  );

            services.AddControllers()
                 .AddNewtonsoftJson(options =>//NuGet:Microsoft.AspNetCore.Mvc.NewtonsoftJson
                 {
                     //忽略空值 不包含属性的null序列化
                     //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                     //忽略默认值和null  1、不包含属性默认值和null
                     //options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Igno
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                     //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                     options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                     options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                     options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
                     options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                 }); ;
            services.AddSwaggerGen();

            // 注册邮件服务
            services.AddTransient<EmailService>();

            //依赖关系注入:控制反转
            DoNetCoreDemo.Service.IoC.AddIoC(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //ASP.NET Core中间件,全局异常处理
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (ex != null)
                    {
                        LogServices.Error($"系统异常: {ex.Message}");
                    }
                });
            });

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            //https://localhost:7132/swagger/index.html
            app.UseSwaggerUI();

            //开始页
            app.UseDefaultFiles();
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new[] { "index.html" }
            });
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //ConventionalRoutingSwaggerGen.UseRoutes(endpoints);
            });
        }
    }
}
