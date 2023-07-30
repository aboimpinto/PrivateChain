using Microsoft.Extensions.Logging;
using PrivateChain.EventMessages;
using PrivateChain.Model;

namespace PrivateChain.Services.MemPool;

public class MemPoolService : 
    IBootstrapper, 
    IMemPoolService,
    IHandle<BlockchainInitializedFromGenesisEvent>,
    IHandle<BlockchainInitializedEvent>
{
    private readonly ILogger<MemPoolService> _logger;
    private readonly IEventAggregator _eventAggregator;
    private string _blockId;
    private string _nextBlockId;
    private string _previousBlockId;
    private IEnumerable<TransactionBase> _nextBlockTransactionsCandidate;
    private IEnumerable<TransactionBase> _allTransactionsCandidate;             // this could be a ConcurrentQueue Collection (FIFO Queue)

    private double _blockIndex = 0;

    public MemPoolService(
        IEventAggregator eventAggregator,
        ILogger<MemPoolService> logger)
    {
        this._eventAggregator = eventAggregator;
        this._logger = logger;

        this._eventAggregator.Subscribe(this);
    }

    public void Shutdown()
    {
    }

    public void Startup()
    {
        this._nextBlockTransactionsCandidate = new List<TransactionBase>();
    }

    public BlockCandidate GetBlockCandidate()
    {
        this._blockIndex ++;

        var blockCandidate = new BlockCandidate(
            this._blockId, 
            this._blockIndex,
            this._previousBlockId,
            Guid.NewGuid().ToString());

        // Generate the BlockId for the next block
        this._previousBlockId = blockCandidate.BlockId;
        this._blockId = blockCandidate.NextBlockId;
        this._nextBlockId = Guid.NewGuid().ToString();

        return blockCandidate;
    }

    public void Handle(BlockchainInitializedFromGenesisEvent message)
    {
        // Crete the first record for the MemPool based on the NextBlockId created in the GenesisBlock
        this._previousBlockId = message.GenesisBlockId;
        this._blockId = message.NextBlockId;
        this._blockIndex = 1;

        this._eventAggregator.PublishAsync(new MemPoolInitializedEvent());
    }

    public void Handle(BlockchainInitializedEvent message)
    {
        // Obtain the MemPool from the Network and keep it updated
    }
}