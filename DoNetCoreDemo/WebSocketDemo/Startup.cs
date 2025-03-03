using Microsoft.OpenApi.Models;
using System.Reflection;
using WebSocketDemo.Extensions;
using WebSocketDemo.Handlers;
using Swashbuckle.AspNetCore.SwaggerGen.ConventionalRouting;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace WebSocketDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = Directory.GetCurrentDirectory();

                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注入WebSocket
            services.AddWebSocketManager();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.TryIncludeXmlComments(XmlCommentsFilePath);

                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction!.ActionName;
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //在controller之前
            app.UseWebSockets();
            // 配置路径
            app.MapSockets("/ws", serviceProvider.GetService<WebSocketMessageHandler>());

            //启用路由
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                ConventionalRoutingSwaggerGen.UseRoutes(endpoints);
            });

            app.UseStaticFiles();

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            //https://localhost:7132/swagger/index.html
            app.UseSwaggerUI();
        }

    }
}
