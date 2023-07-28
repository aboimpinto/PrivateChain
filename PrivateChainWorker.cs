using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PrivateChain
{
    public class PrivateChainWorker : BackgroundService
    {
        private readonly ILogger<PrivateChainWorker> _logger;
        public PrivateChainWorker(ILogger<PrivateChainWorker> logger)
        {
            this._logger = logger;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("PrivateChain service worker started...");

            return Task.CompletedTask;
        }
    }
}