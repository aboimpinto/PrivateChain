namespace PrivateChain;

public interface IHandle<T>
{
    void Handle(T message);
}
