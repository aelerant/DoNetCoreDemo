using Microsoft.Extensions.DependencyInjection;

namespace DoNetCoreDemo.Service
{
    public class IoC
    {
        public static void AddIoC(IServiceCollection services)
        {
            services.AddTransient<IAPIManager, APIManager>();
        }
    }
}