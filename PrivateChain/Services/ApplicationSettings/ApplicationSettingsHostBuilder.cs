using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrivateChain.Model.ApplicationSettings;

namespace PrivateChain.Services.ApplicationSettings
{
    public static class ApplicationSettingsHostBuilder
    {
        public static IHostBuilder RegisterApplicationSettings(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) => 
        {
            services.AddSingleton<IStackerInfo, StackerInfo>();
            services.AddSingleton<IServerInfo, ServerInfo>();
            services.AddSingleton<IBootstrapper, ApplicationSettingsService>(); 
        });

        return builder;
    }   
    }
}