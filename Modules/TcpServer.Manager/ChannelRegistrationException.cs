namespace TcpServer.Manager;

public class ChannelRegistrationException : Exception
{
    public ChannelRegistrationException(string message) : base(message)
    {
    }
}