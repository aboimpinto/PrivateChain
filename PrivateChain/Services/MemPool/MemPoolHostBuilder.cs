using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Olimpo;

namespace PrivateChain.Services.MemPool;

public static class MemPoolHostBuilder
{
    public static IHostBuilder RegisterMemPool(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) => 
        {
            services.AddSingleton<IMemPoolService, MemPoolService>();

            var serviceProvider = services.BuildServiceProvider();

            var memPoolService = serviceProvider.GetService<IMemPoolService>();
            if (memPoolService is IBootstrapper)
            {
                services.AddSingleton<IBootstrapper>((IBootstrapper)memPoolService);
            }
        });

        return builder;
    }    
}
