namespace PrivateChain;

public interface IHandleAsync<T>
{
    Task HandleAsync(T message);
}
