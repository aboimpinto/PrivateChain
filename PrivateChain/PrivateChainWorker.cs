using Olimpo;
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("PrivateChain service worker started...");

            this._logger.LogInformation("Start bootstrappable modules...");
            var bootstrappers = this._bootstrappers.OrderBy(x => x.Priority);
            foreach(var module in bootstrappers)
            {
                module.Startup();
            }

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Stopping modules...");

            var bootstrappers = this._bootstrappers.OrderBy(x => x.Priority);
            foreach(var module in bootstrappers)
            {
                module.Shutdown();
            }

            this._logger.LogInformation("All modules successfully shutdown...");
            return base.StopAsync(cancellationToken);
        }
    }
}