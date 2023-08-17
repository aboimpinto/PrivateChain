using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PrivateChain.Model.ApplicationSettings;

namespace PrivateChain.Services.ApplicationSettings
{
    public class ApplicationSettingsService : IBootstrapper
    {
        private readonly IStackerInfo _stackerInfo;
        private readonly ILogger<ApplicationSettingsService> _logger;

        public ApplicationSettingsService(
            IStackerInfo stackerInfo,
            ILogger<ApplicationSettingsService> logger)
        {
            this._stackerInfo = stackerInfo;
            this._logger = logger;
        }

        public int Priority { get; set; } = 0;

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

            var stackerInfo = config
                .GetRequiredSection("StackerInfo")
                .Get<StackerInfo>();

            this._stackerInfo.PublicEncryptAddress = stackerInfo.PublicEncryptAddress;
            this._stackerInfo.PublicSigningAddress = stackerInfo.PublicSigningAddress;
        }
    }
}