using Olimpo;
using Olimpo.RPC.Model;
using PrivateChain.Services.Server.Events;

namespace PrivateChain.Services.Server.CommandProcesssors;

public class HandShakeCommandProcessorStrategy : ICommandProcessor
{
    private readonly IEventAggregator _eventAggregator;

    public HandShakeCommandProcessorStrategy(IEventAggregator eventAggregator)
    {
        this._eventAggregator = eventAggregator;
    }

    public bool CanHandle(CommandBase command)
    {
        if (command.Command == "HandShakeCommand")
        {
            return true;
        }

        return false;
    }

    public async Task Handle(CommandBase commandBase, string channelId)
    {
        await this._eventAggregator.PublishAsync(new HandShakeMessageReceived((HandshakeCommand)commandBase, channelId));
    }
}
