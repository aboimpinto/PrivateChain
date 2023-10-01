using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Olimpo;

namespace PrivateChain.Services.Listener;
public static class ListenerHostBuilder
{
    public static IHostBuilder RegisterListener(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) => 
        {
            services.AddSingleton<IBootstrapper, ListenerService>();
        });

        return builder;
    }
}
