namespace PrivateChain.Model
{
    public class Transaction
    {
        public TransactionBase RawTransaction { get; }

        public string Signature { get; }

        public Transaction(TransactionBase transactionBase, string signature)
        {
            this.RawTransaction = transactionBase;
            this.Signature = signature;
        }
    }
}