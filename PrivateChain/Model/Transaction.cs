namespace PrivateChain.Model
{
    public class Transaction
    {
        public TransactionBase RawTransaction { get; }

        public string Signature { get; }

        public bool IsEncrypted { get; }

        public bool IsValueTransaction { get; }

        public Transaction(
            TransactionBase transactionBase, 
            string signature, 
            bool isEncrypted = false, 
            bool isValueTransaction = false)
        {
            if (isValueTransaction && IsEncrypted)
            {
                throw new InvalidOperationException("Value Transaction cannot be Encrypted.");
            }

            this.RawTransaction = transactionBase;
            this.Signature = signature;

            this.IsEncrypted = isEncrypted;
            this.IsValueTransaction = isValueTransaction;
        }
    }
}