using Olimpo.RPC.Model;

namespace PrivateChain.Services.Server.Events;

public class HandShakeMessageReceived
{
    public HandshakeCommand HandshakeCommand { get; set; }

    public string ChannelId { get; set; } = string.Empty;

    public HandShakeMessageReceived(HandshakeCommand command, string channelId)
    {
        this.HandshakeCommand = command;
        this.ChannelId = channelId;
    }
}
