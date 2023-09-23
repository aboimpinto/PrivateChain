namespace PrivateChain.Model.ApplicationSettings
{
    public interface IStackerInfo
    {
        string PublicSigningAddress { get; set; }

        string PrivateSigningAddress { get; set; }

        string PublicEncryptAddress { get; set; }

        string PrivateEncryptAddress { get; set; }
    }
}