namespace PrivateChain.Model
{
    public interface ISpecificTransactionDeserializer
    {
        bool CanHandle(string transactionType);

        TransactionBase Handle(string rawText);
    }
}