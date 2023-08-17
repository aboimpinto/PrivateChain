namespace PrivateChain.Model.ApplicationSettings
{
    public class StackerInfo : IStackerInfo
    {
        public string PublicSigningAddress { get; set; } = string.Empty;

        public string PublicEncryptAddress { get; set; } = string.Empty;
    }
}