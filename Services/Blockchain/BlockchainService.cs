using PrivateChain.Builders;
using PrivateChain.EventMessages;
using PrivateChain.Model;

namespace PrivateChain.Services.Blockchain;

public class BlockchainService : 
    IBootstrapper, 
    IBlockchainService,
    IHandle<BlockCreatedEvent>
{
    public BlockchainInfo BlockchainInfo { get; private set; }

    private List<Block> _blockchain;
    private string _lastBlockIdFromLocal = string.Empty;
    private readonly IEventAggregator _eventAggregator;

    public BlockchainService(IEventAggregator eventAggregator)
    {
        this._eventAggregator = eventAggregator;

        this._blockchain = new List<Block>();

        this._eventAggregator.Subscribe(this);
    }

    public int Priority { get; set; } = 10;

    public void Shutdown()
    {
    }

    public void Startup()
    {
        // Connect to the Network and obtain the last block
        var lastBlockIdFromNetwork = string.Empty;

        // Load local Blockchain and obtain the last block 
        this._lastBlockIdFromLocal = string.Empty;

        if (string.IsNullOrEmpty(lastBlockIdFromNetwork) && string.IsNullOrEmpty(this._lastBlockIdFromLocal))
        {
            // because it's the GenesisBlock there is nothing in the MemPool 
            var genesisBlockId = Guid.NewGuid().ToString();

            // Creation of the GenesisBlock
            var block = new BlockBuilder()
                .WithBlockId(genesisBlockId)
                .WithNextBlockId(Guid.NewGuid().ToString())
                .WithIndex(1)
                .Build();

            block.FinalizeBlock();

            this._blockchain.Add(block);
            this._lastBlockIdFromLocal = genesisBlockId;

            this._eventAggregator.PublishAsync(new BlockchainInitializedFromGenesisEvent(block.BlockId, block.NextBlockId));
        }
        else
        {
            // Need to sync the Local Blockchain

            this._eventAggregator.PublishAsync(new BlockchainInitializedEvent());
        }
    }

    public void Handle(BlockCreatedEvent message)
    {
        this._blockchain.Add(message.Block);
    }
}
