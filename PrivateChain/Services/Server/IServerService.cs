namespace PrivateChain.Services.Server
{
    public interface IServerService
    {
        Task Start();

        void Stop();
    }
}