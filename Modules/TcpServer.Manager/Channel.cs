using System.Net.Sockets;
using System.Text;

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

        string data;
        using(this._stream = this._client.GetStream())
        {
            var position = 0;

            while(this._isOpen)
            {
                if (this._client.IsClientDisconnected())
                {
                    this.Close();
                }
                else
                {
                    while((position = this._stream.Read(this._buffer, 0, this._buffer.Length)) != 0 && this._isOpen)
                    {
                        data = Encoding.UTF8.GetString(this._buffer, 0, position);

                        this._server.DataReceived.OnNext(new DataReceivedArgs(this._channelId, data, this));

                        if (!this._isOpen)
                        {
                            break;
                        }
                    }
                }
            }
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
            
            this._stream.Close();
            this._client.Close();
            this._disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Close()
    {
        this.Dispose(false);
        this._isOpen = false;   
        this._server.ConnectedChannels.OpenChannels.TryRemove(this._channelId, out Channel removedChannel);
    }
}