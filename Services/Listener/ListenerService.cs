using Microsoft.Extensions.Logging;

namespace PrivateChain.Services.Listener;

public class ListenerService : IListenerService, IBootstrapper
{
    private ILogger<ListenerService> _logger;

    public ListenerService(ILogger<ListenerService> logger)
    {
        this._logger = logger;
    }

    public Task ShutdownAsync()
    {
        return Task.CompletedTask;
    }

    public Task StartupAsync()
    {
        this._logger.LogInformation("Starting ListenerService...");

        return Task.CompletedTask;
    }
}