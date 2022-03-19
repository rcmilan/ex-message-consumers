using Microsoft.Extensions.DependencyInjection;
using Shared.MQ.Services;

namespace Shared.MQ
{
    public static class ModuleDependency
    {
        public static void AddMQModule(this IServiceCollection services)
        {
            services
                .AddSingleton(new MQConfig("localhost").BuildModel)
                .AddScoped<IMQService, MQService>()
                ;
        }
    }
}