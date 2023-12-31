using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Olimpo;
using PrivateChain.Model;

namespace PrivateChain.Services.BlockGenerator;

public static class BlockGeneratorHostBuilder
{
    public static IHostBuilder RegisterBlockGenerator(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) => 
        {
            services.AddSingleton<IBootstrapper, BlockGeneratorService>();
        });

        return builder;
    }
}