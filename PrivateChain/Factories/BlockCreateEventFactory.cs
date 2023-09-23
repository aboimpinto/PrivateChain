using PrivateChain.EventMessages;
using PrivateChain.Model;

namespace PrivateChain.Factories;

public class BlockCreateEventFactory : IBlockCreateEventFactory
{
    private readonly TransactionBaseConverter _transactionBaseConverter;
    
    public BlockCreateEventFactory(TransactionBaseConverter transactionBaseConverter)
    {
            this._transactionBaseConverter = transactionBaseConverter;
    }

    public BlockCreatedEvent GetInstance(Block block)
    {
        return new BlockCreatedEvent(block, this._transactionBaseConverter);
    }   
}
