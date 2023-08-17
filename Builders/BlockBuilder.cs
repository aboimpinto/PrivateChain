using PrivateChain.Model;

namespace PrivateChain.Builders
{
    public class BlockBuilder
    {
        private string _previousBlockId = string.Empty;
        private string _nextBlockId = string.Empty;
        private string _blockId = string.Empty;
        private double _index;
        private BlockCreationTransaction _rewardTrasaction;

        public BlockBuilder WithBlockId(string blockId)
        {
            this._blockId = blockId;
            return this;
        }

        public BlockBuilder WithRewardBeneficiary(string beneficiaryAddress)
        {
            this._rewardTrasaction = new BlockCreationTransaction();

            this._rewardTrasaction.DestinationAddress = beneficiaryAddress;
            this._rewardTrasaction.Reward = 0.5;              // TODO: this need to be configurable by the network,

            return this;
        }

        public BlockBuilder WithPreviousBlockId(string previousBlockId)
        {
            this._previousBlockId = previousBlockId;
            return this;
        }

        public BlockBuilder WithNextBlockId(string nextBlockId)
        {
            this._nextBlockId = nextBlockId;
            return this;
        }

        public BlockBuilder WithIndex(double index)
        {
            this._index = index;
            return this;
        }

        public Block Build()
        {
            var block = new Block(
                this._blockId, 
                this._previousBlockId, 
                this._nextBlockId, 
                this._index);

            block.Transactions = new List<TransactionBase>
            {
                this._rewardTrasaction
            };

            return block;
        }
    }
}