using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Olimpo;
using Olimpo.RPC.Model;
using Olimpo.TcpServer.Manager;
using PrivateChain.Model.ApplicationSettings;
using PrivateChain.Services.Server.Events;
using RPC.Model;

namespace PrivateChain.Services.Server
{
    public class ServerService : 
        IServerService,
        IHandle<HandShakeMessageReceived>
    {
        private readonly IServerInfo _serverInfo;
        private readonly IServer _server;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger<ServerService> _logger;
        private CancellationTokenSource _serverCancelationTokenSouce;

        public ServerService(
            IServerInfo serverInfo,
            IServer server,
            IEventAggregator eventAggregator,
            ILogger<ServerService> logger)
        {
            this._serverInfo = serverInfo;
            this._server = server;
            this._eventAggregator = eventAggregator;
            this._logger = logger;

            this._serverCancelationTokenSouce = new CancellationTokenSource();

            this._eventAggregator.Subscribe(this);
        }

        public async Task Start()
        {
            this._server.DataReceived.Subscribe(x => 
            {
                this._logger.LogInformation($"Message: {x.Message} from {x.ChannelId} received...");    

                var jsonOptions = new JsonSerializerOptions
                {
                    Converters = { new CommandBaseConverter() }
                };

                var command = JsonSerializer.Deserialize<CommandBase>(x.Message, jsonOptions);

                // TODO [AboimPinto]: Need to find a solution for injecting here strategies for what to do for each command.
                if (command.Command == Commands.HandshakeCommand)
                {
                    this._eventAggregator.PublishAsync(new HandShakeMessageReceived((HandshakeCommand)command, x.ChannelId));
                }
            });

            await this._server.Start(IPAddress.Any, this._serverInfo.ListeningPort);
        }

        public void Stop()
        {
            this._logger.LogInformation("Stopping TCP Server...");
            this._serverCancelationTokenSouce.Cancel();
        }

        public void Handle(HandShakeMessageReceived message)
        {
            if (!this._server.ConnectedChannels.OpenChannels.Any(x => x.Key == message.ChannelId))
            {
                // the channel doesnt'e exist or it's been disconnected.
            }

            if (this._server.ConnectedChannels.OpenChannels.Count(x => x.Key == message.ChannelId) > 1)
            {
                throw new InvalidOperationException("It's not possible to have more than one channel open for the same ChannelId");
            }

            var channel = this._server.ConnectedChannels.OpenChannels
                .Single(x => x.Key == message.ChannelId).Value;
            
            channel.Send(new HandshakeSuccessfulResponse().ToJson());

            // channel.Send(message.HandshakeCommand.)
        }

        private void ManageClientConnection(object obj)
        {
            var client = (TcpClient)obj;    
        }
    }
}