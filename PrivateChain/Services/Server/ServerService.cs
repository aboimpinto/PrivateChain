using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using PrivateChain.Model.ApplicationSettings;

namespace PrivateChain.Services.Server
{
    public class ServerService : IServerService
    {
        private readonly IServerInfo _serverInfo;
        private readonly ILogger<ServerService> _logger;
        private bool _isServerStarted;
        private TcpListener _server;
        private CancellationTokenSource _serverCancelationTokenSouce;

        public ServerService(
            IServerInfo serverInfo,
            ILogger<ServerService> logger)
        {
            this._serverInfo = serverInfo;
            this._logger = logger;

            this._serverCancelationTokenSouce = new CancellationTokenSource();
        }

        public async Task Start()
        {
            if (!this._isServerStarted)
            {
                this._isServerStarted = true;

                this._server = new TcpListener(IPAddress.Any, this._serverInfo.ListeningPort);
                this._server.Start();
                this._logger.LogInformation("Server listening...");

                while(!this._serverCancelationTokenSouce.IsCancellationRequested)
                {
                    var client = await this._server.AcceptTcpClientAsync();
                    ThreadPool.QueueUserWorkItem(this.ManageClientConnection, client);
                }
            }
        }

        public void Stop()
        {
            this._logger.LogInformation("Stopping TCP Server...");
            this._serverCancelationTokenSouce.Cancel();
        }

        private void ManageClientConnection(object obj)
        {
            
        }
    }
}