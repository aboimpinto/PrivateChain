using Olimpo;

namespace PrivateChain.Services.Server;

public class ServerBootstrapper : IBootstrapper
{
    public int Priority { get; set; } = 10;
    private readonly IServerService _serverService;

    public ServerBootstrapper(IServerService serverService)
    {
        this._serverService = serverService;
    }

    public void Shutdown()
    {
        this._serverService.Stop();
    }

    public async Task Startup()
    {
        // TODO: the server should only start when is fully synchronized.
        await this._serverService.Start();
    }
}
