using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PrivateChain
{
    public class PrivateChainWorker : BackgroundService
    {
        private readonly ILogger<PrivateChainWorker> _logger;
        private readonly IEnumerable<IBootstrapper> _bootstrappers;

        public PrivateChainWorker(
            IEnumerable<IBootstrapper> bootstrappers, 
            ILogger<PrivateChainWorker> logger)
        {
            this._bootstrappers = bootstrappers;
            this._logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("PrivateChain service worker started...");

            this._logger.LogInformation("Start bootstrappable modules...");

            foreach(var module in this._bootstrappers)
            {
                await module.StartupAsync();
            }
        }
    }
}