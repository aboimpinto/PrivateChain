using Microsoft.Extensions.Logging;

namespace PrivateChain.Services.BlockGenerator;

public class BlockGeneratorService : IBlockGeneratorService, IBootstrapper
{
    private readonly ILogger<BlockGeneratorService> _logger;

    public BlockGeneratorService(ILogger<BlockGeneratorService> logger)
    {
        this._logger = logger;
    }

    public Task ShutdownAsync()
    {
        return Task.CompletedTask;
    }

    public Task StartupAsync()
    {
        this._logger.LogInformation("Starting BlockGeneratorService...");

        return Task.CompletedTask;
    }
}