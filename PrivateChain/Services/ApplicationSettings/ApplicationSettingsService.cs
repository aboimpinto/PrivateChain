using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PrivateChain.Model.ApplicationSettings;

namespace PrivateChain.Services.ApplicationSettings
{
    public class ApplicationSettingsService : IBootstrapper
    {
        private readonly IStackerInfo _stackerInfo;
        private readonly IServerInfo _serverInfo;
        private readonly ILogger<ApplicationSettingsService> _logger;

        public ApplicationSettingsService(
            IStackerInfo stackerInfo,
            IServerInfo serverInfo,
            ILogger<ApplicationSettingsService> logger)
        {
            this._stackerInfo = stackerInfo;
            this._serverInfo = serverInfo;
            this._logger = logger;
        }

        public int Priority { get; set; } = 0;

        public void Shutdown()
        {
        }

        public Task Startup()
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

            if (stackerInfo == null)
            {
                throw new InvalidOperationException("Missing stacker information in ApplicationSetting.json");
            }

            var serverInfo = config
                .GetRequiredSection("ServerInfo")
                .Get<ServerInfo>();

            if (serverInfo == null)
            {
                throw new InvalidOperationException("Missing server information in ApplicationSetting.json");
            }

            this._stackerInfo.PublicEncryptAddress = stackerInfo.PublicEncryptAddress;
            this._stackerInfo.PublicSigningAddress = stackerInfo.PublicSigningAddress;

            this._serverInfo.ListeningPort = serverInfo.ListeningPort;

            return Task.CompletedTask;
        }
    }
}