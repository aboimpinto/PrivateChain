namespace PrivateChain
{
    public interface IBootstrapper
    {
        Task StartupAsync();

        Task ShutdownAsync();
    }
}