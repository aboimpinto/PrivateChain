using System.Net;
using System.Net.Sockets;

namespace TcpServer.Manager;

public class Server : IServer
{
    private TcpListener _listener;

    public bool Running { get; set; }

    public Channels ConnectedChannels { get; private set; }

    public Server()
    {
        this.ConnectedChannels = new Channels(this);
    }

    public async Task Start(string address, int port)
    {
        await this.Start(IPAddress.Parse(address), port);
    }

    public async Task Start(IPAddress address, int port)
    {
        this._listener = new TcpListener(address, port);

        try
        {
            this._listener.Start();
            this.Running = true;

            while(this.Running)
            {
                var client = await this._listener.AcceptTcpClientAsync();
                // await Task.Run(() => new Channel(this).Open(client))
                ThreadPool.QueueUserWorkItem(this.CreateChannel, client);
            }
        }
        catch(SocketException)
        {
            throw;
        }
        catch(ChannelRegistrationException)
        {
            throw;
        }
    }

    public void Stop()
    {
    }

    private void CreateChannel(object obj)
    {
        new Channel(this).Open((TcpClient)obj);
    }
}