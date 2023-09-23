using Microsoft.Extensions.Logging;

namespace PrivateChain.Services.Listener;

public class ListenerService : IListenerService, IBootstrapper
{
    private ILogger<ListenerService> _logger;

    public ListenerService(ILogger<ListenerService> logger)
    {
        this._logger = logger;
    }

    public int Priority { get; set; } = 10;

    public void Shutdown()
    {
    }

    public Task Startup()
    {
        this._logger.LogInformation("Starting ListenerService...");

        return Task.CompletedTask;
    }
}