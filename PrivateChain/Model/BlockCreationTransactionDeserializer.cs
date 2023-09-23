using System.Text.Json;

namespace PrivateChain.Model
{
    public class BlockCreationTransactionDeserializer : ISpecificTransactionDeserializer
    {
        public bool CanHandle(string transactionType)
        {
            if (transactionType== NativeTransactionType.BlockCreation)
            {
                return true;
            }

            return false;
        }

        public TransactionBase Handle(string rawText)
        {
            return JsonSerializer.Deserialize<BlockCreationTransaction>(rawText);
        }
    }
}