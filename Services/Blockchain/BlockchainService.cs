using Microsoft.Extensions.Logging;
using PrivateChain.Builders;
using PrivateChain.EventMessages;
using PrivateChain.Model;
using PrivateChain.Model.ApplicationSettings;

namespace PrivateChain.Services.Blockchain;

public class BlockchainService : 
    IBootstrapper, 
    IHandle<BlockCreatedEvent>
{
    public BlockchainInfo BlockchainInfo { get; private set; }

    private List<Block> _blockchain;
    private string _lastBlockIdFromLocal = string.Empty;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<BlockchainService> _logger;
    private readonly IStackerInfo _stackerInfo;

    public BlockchainService(
        IStackerInfo stackerInfo,
        IEventAggregator eventAggregator,
        ILogger<BlockchainService> logger)
    {
        this._stackerInfo = stackerInfo;
        this._eventAggregator = eventAggregator;
        this._logger = logger;

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
                .WithRewardBeneficiary(this._stackerInfo.PublicSigningAddress)
                .WithIndex(1)
                .Build();

            block.FinalizeBlock();

            this._logger.LogInformation("Genesis block created: {0}", block.ToString());

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
