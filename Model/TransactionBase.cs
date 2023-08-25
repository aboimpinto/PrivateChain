namespace PrivateChain.Model
{
    public abstract class TransactionBase
    {
        public string TransactionId { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool Encrypted { get; set; } = false;

        public string BlockId { get; set; } = string.Empty;
    }
}