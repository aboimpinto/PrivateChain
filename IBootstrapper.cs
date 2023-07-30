namespace PrivateChain
{
    public interface IBootstrapper
    {
        int Priority { get; set; }

        void Startup();

        void Shutdown();
    }
}