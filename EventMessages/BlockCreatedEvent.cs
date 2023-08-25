using System.Text.Json;
using PrivateChain.Model;

namespace PrivateChain.EventMessages
{
    public class BlockCreatedEvent
    {
        private readonly TransactionBaseConverter _transactionBaseConverter;

        public Block Block { get; }

        public BlockCreatedEvent(
            Block block, 
            TransactionBaseConverter transactionBaseConverter)
        {
            this.Block = block;
            this._transactionBaseConverter = transactionBaseConverter;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                Converters = { this._transactionBaseConverter }
            });
        }
    }
}