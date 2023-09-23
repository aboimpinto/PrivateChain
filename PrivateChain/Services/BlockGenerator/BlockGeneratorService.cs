using Microsoft.Extensions.Logging;
using PrivateChain.EventMessages;
using PrivateChain.Factories;
using PrivateChain.Model.ApplicationSettings;
using PrivateChain.Services.MemPool;
using System.Reactive.Linq;

namespace PrivateChain.Services.BlockGenerator;

public class BlockGeneratorService : 
    IBootstrapper,
    IHandle<BlockchainInitializedEvent>,
    IHandle<MemPoolInitializedEvent>
{
    private readonly IMemPoolService _memPoolService;
    private readonly IStackerInfo _stackerInfo;
    private readonly IBlockCreateEventFactory _blockCreateEventFactory;
    private readonly IBlockBuilderFactory _blockBuilderFactory;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<BlockGeneratorService> _logger;
    
    private IObservable<long> _blockchainGeneratorLoop;
    private int _blockNbr;

    public BlockGeneratorService(
        IMemPoolService memPoolService, 
        IStackerInfo stackerInfo,
        IBlockCreateEventFactory blockCreateEventFactory,
        IBlockBuilderFactory blockBuilderFactory,
        IEventAggregator eventAggregator,
        ILogger<BlockGeneratorService> logger)
    {
        this._memPoolService = memPoolService;
        this._stackerInfo = stackerInfo;
        this._blockCreateEventFactory = blockCreateEventFactory;
        this._blockBuilderFactory = blockBuilderFactory;
        this._eventAggregator = eventAggregator;
        this._logger = logger;

        this._eventAggregator.Subscribe(this);
    }

    public int Priority { get; set; } = 5;

    public void Shutdown()
    {
    }

    public Task Startup()
    {
        this._logger.LogInformation("Starting BlockGeneratorService...");

        this._blockchainGeneratorLoop = Observable
            .Interval(TimeSpan.FromSeconds(3));

            return Task.CompletedTask;
    }

    public void Handle(BlockchainInitializedEvent message)
    {
        
    }

    public void Handle(MemPoolInitializedEvent message)
    {
        this._blockchainGeneratorLoop.Subscribe(x => 
        {
            var blockCandidate = this._memPoolService.GetBlockCandidate();

            var block = this._blockBuilderFactory.GetInstance()
                .WithBlockId(blockCandidate.BlockId)
                .WithPreviousBlockId(blockCandidate.PreviousBlockId)
                .WithRewardBeneficiary(this._stackerInfo)
                .WithNextBlockId(blockCandidate.NextBlockId)
                .WithIndex(blockCandidate.Index)
                .Build();

            this._eventAggregator.PublishAsync(this._blockCreateEventFactory.GetInstance(block));
        });
    }
}