using System.Text.Json;
using PrivateChain.Model;
using PrivateChain.Model.ApplicationSettings;

namespace PrivateChain.Builders
{
    public class BlockBuilder
    {
        private readonly TransactionBaseConverter _transactionBaseConverter;
        private string _previousBlockId = string.Empty;
        private string _nextBlockId = string.Empty;
        private string _blockId = string.Empty;
        private double _index;
        private Transaction _rewardTrasaction;

        public BlockBuilder(TransactionBaseConverter transactionBaseConverter)
        {
            this._transactionBaseConverter = transactionBaseConverter;
        }

        public BlockBuilder WithBlockId(string blockId)
        {
            this._blockId = blockId;
            return this;
        }

        public BlockBuilder WithRewardBeneficiary(IStackerInfo stackerInfo)
        {
            var rawRewardTrasaction = new BlockCreationTransaction
            {
                Type = NativeTransactionType.BlockCreation,
                BlockId = this._blockId,
                TransactionId = Guid.NewGuid().ToString(),
                DestinationAddress = stackerInfo.PublicEncryptAddress,
                Reward = 0.5                                                // TODO: this need to be configurable by the network,
            };

            var jsonOptions = new JsonSerializerOptions
            {
                Converters = { this._transactionBaseConverter }
            };

            var jsonRewardTransaction = JsonSerializer.Serialize(this._rewardTrasaction, jsonOptions);
            var rewardTransactionSignature = Signing.Manager.SigningKeys.SignMessage(jsonRewardTransaction, stackerInfo.PrivateSigningAddress);

            this._rewardTrasaction = new Transaction(
                rawRewardTrasaction, 
                rewardTransactionSignature,
                isEncrypted: false,
                isValueTransaction: true);

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
            if (this._rewardTrasaction == null)
            {
                throw new InvalidOperationException("Cannot create a block without reward transaction!");
            }

            var block = new Block(
                this._blockId,
                this._previousBlockId,
                this._nextBlockId,
                this._index)
            {
                Transactions = new List<Transaction>
                {
                    this._rewardTrasaction
                }
            };

            return block;
        }
    }
}