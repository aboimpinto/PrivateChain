namespace PrivateChain.Model.ApplicationSettings
{
    public interface IStackerInfo
    {
        string PublicSigningAddress { get; set; }

        string PublicEncryptAddress { get; set; }
    }
}