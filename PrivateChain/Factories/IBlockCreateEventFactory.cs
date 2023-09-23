using PrivateChain.EventMessages;
using PrivateChain.Model;

namespace PrivateChain.Factories;

public interface IBlockCreateEventFactory
{
    BlockCreatedEvent GetInstance(Block block);
}