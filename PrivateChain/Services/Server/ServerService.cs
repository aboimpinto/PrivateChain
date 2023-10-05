using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using PrivateChain.Model.ApplicationSettings;
using TcpServer.Manager;

namespace PrivateChain.Services.Server
{
    public class ServerService : IServerService
    {
        private readonly IServerInfo _serverInfo;
        private readonly IServer _server;
        private readonly ILogger<ServerService> _logger;
        private bool _isServerStarted;
        
        private CancellationTokenSource _serverCancelationTokenSouce;

        public ServerService(
            IServerInfo serverInfo,
            IServer server,
            ILogger<ServerService> logger)
        {
            this._serverInfo = serverInfo;
            this._server = server;
            this._logger = logger;

            this._serverCancelationTokenSouce = new CancellationTokenSource();
        }

        public async Task Start()
        {
            this._server.DataReceived.Subscribe(x => 
            {
                this._logger.LogInformation($"Message: {x.Message} from {x.ChannelId} received...");    
            });

            await this._server.Start(IPAddress.Any, this._serverInfo.ListeningPort);
        }

        public void Stop()
        {
            this._logger.LogInformation("Stopping TCP Server...");
            this._serverCancelationTokenSouce.Cancel();
        }

        private void ManageClientConnection(object obj)
        {
            var client = (TcpClient)obj;    
        }
    }
}