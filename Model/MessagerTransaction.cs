namespace PrivateChain.Model
{
    public class MessageTransaction : TransactionBase
    {
        public string Destination { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}