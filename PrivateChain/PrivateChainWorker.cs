using Olimpo;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PrivateChain
{
    public class PrivateChainWorker : BackgroundService
    {
        private readonly ILogger<PrivateChainWorker> _logger;
        private readonly IBootstrapperManager _bootstrapperManager;

        public PrivateChainWorker(
            IBootstrapperManager bootstrapperManager,
            ILogger<PrivateChainWorker> logger)
        {
            this._bootstrapperManager = bootstrapperManager;
            this._logger = logger;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("PrivateChain service worker started...");

            this._bootstrapperManager.StartBootstrappableModules();

            // this._logger.LogInformation("Start bootstrappable modules...");
            // var bootstrappers = this._bootstrappers.OrderBy(x => x.Priority);
            // foreach(var module in bootstrappers)
            // {
            //     module.Startup();
            // }

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Stopping modules...");

            // var bootstrappers = this._bootstrappers.OrderBy(x => x.Priority);
            // foreach(var module in bootstrappers)
            // {
            //     module.Shutdown();
            // }

            this._logger.LogInformation("All modules successfully shutdown...");
            return base.StopAsync(cancellationToken);
        }
    }
}