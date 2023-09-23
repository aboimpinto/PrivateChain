using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrivateChain.Services.Server
{
    public static class ServerHostBuilder
    {
        public static IHostBuilder RegisterServer(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) => 
        {
            services.AddSingleton<IBootstrapper, ServerBootstrapper>();
            services.AddSingleton<IServerService, ServerService>();
        });

        return builder;
    }   
    }
}