namespace PrivateChain;

public interface IEventAggregator
{
    void Subscribe(object subscriber);

    Task PublishAsync<T>(T message) where T : class;
}