using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrivateChain.Model.ApplicationSettings;

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
    }
}