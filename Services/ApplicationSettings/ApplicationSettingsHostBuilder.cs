using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PrivateChain.Services.ApplicationSettings
{
    public static class ApplicationSettingsHostBuilder
    {
        public static IHostBuilder RegisterApplicationSettings(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) => 
        {
            services.AddSingleton<IApplicationSettingsService, ApplicationSettingsService>(); 

            var serviceProvider = services.BuildServiceProvider();

            var applicationSettingsService = serviceProvider.GetService<IApplicationSettingsService>();
            if (applicationSettingsService is IBootstrapper)
            {
                services.AddSingleton<IBootstrapper>((IBootstrapper)applicationSettingsService);
            }
        });

        return builder;
    }   
    }
}