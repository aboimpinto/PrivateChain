using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PrivateChain.Model.ApplicationSettings;

namespace PrivateChain.Services.ApplicationSettings
{
    public class ApplicationSettingsService : IBootstrapper, IApplicationSettingsService
    {
        private readonly ILogger<ApplicationSettingsService> _logger;

        public ApplicationSettingsService(ILogger<ApplicationSettingsService> logger)
        {
            this._logger = logger;
        }

        public int Priority { get; set; } = 0;

        public StackerInfo StackerInfo { get; set; }

        public void Shutdown()
        {
        }

        public void Startup()
        {
            this._logger.LogInformation("Load ApplicationSettings");
            var applicationSettingsFile = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                "ApplicationSettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonFile(applicationSettingsFile)
                .AddEnvironmentVariables()
                .Build();

            this.StackerInfo = config
                .GetRequiredSection("StackerInfo")
                .Get<StackerInfo>();
        }
    }
}