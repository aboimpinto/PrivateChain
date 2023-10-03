using System.Collections.Concurrent;

namespace TcpServer.Manager;

public class Channels
{
    private readonly Server _server;

    public ConcurrentDictionary<string, Channel> OpenChannels { get; }    

    public Channels(Server myServer)
    {
        this.OpenChannels = new ConcurrentDictionary<string, Channel>();
        this._server = myServer;
    }
}