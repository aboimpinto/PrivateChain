using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Olimpo;

namespace PrivateChain.Services.Blockchain;

public static class BlockchainHostBuilder
{
    public static IHostBuilder RegisterBlockchain(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) => 
        {
            services.AddSingleton<IBootstrapper, BlockchainService>();
        });

        return builder;
    }   
}