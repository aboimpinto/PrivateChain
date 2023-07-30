namespace PrivateChain.Model
{
    public abstract class TransactionBase
    {
        public string TransactionId { get; set; } = string.Empty;

        public TransactionType Type { get; set; }

        public string Origin { get; set; } = string.Empty;

        public bool Encrypted { get; set; } = false;

        public string BlockId { get; set; } = string.Empty;
    }
}