using System.Net;

namespace TcpServer.Manager;

public interface IServer
{
    bool Running {  get; set; }

    Channels ConnectedChannels { get; }

    Task Start(string address, int port);

    Task Start(IPAddress address, int port);

    void Stop();
}