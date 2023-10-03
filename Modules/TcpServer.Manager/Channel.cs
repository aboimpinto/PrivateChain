using System.Net.Sockets;
using System.Threading.Channels;

namespace TcpServer.Manager;

public class Channel : IDisposable
{
    private TcpClient _client;
    private Server _server;
    private readonly byte[] _buffer;
    private readonly string _channelId;
    private NetworkStream _stream;
    private bool _isOpen;
    private bool _disposed;

    public Channel(Server myServer)
    {
        this._server = myServer;
        
        this._buffer = new byte[256];
        this._channelId = Guid.NewGuid().ToString();
    }

    public void Open(TcpClient client)
    {
        this._client = client;
        this._isOpen = true;

         if (!this._server.ConnectedChannels.OpenChannels.TryAdd(this._channelId, this))
         {
            this._isOpen = false;
            throw (new ChannelRegistrationException("Unable to register a new communication channel to channel list."));
         }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }
            // stream.Close();
            this._client.Close();
            this._disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}