using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace PrivateChain;

public class EventAggregator : IEventAggregator
{
    private readonly IDictionary<Type, object> _subscribersList = new Dictionary<Type, object>();
    private readonly ILogger<EventAggregator> _logger;

    public EventAggregator(ILogger<EventAggregator> logger)
    {
        this._logger = logger;
    }

    public void Subscribe(object subscriber)
    {
        if (!this._subscribersList.Any(x => x.Key == subscriber.GetType()))
        {
            this._subscribersList.Add(subscriber.GetType(), subscriber);
        }
    }

    public Task PublishAsync<T>(T message) where T : class
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        this._logger.LogInformation("Publishing message {0} | {1}", message.GetType().Name, jsonMessage);

        foreach (var handler in this._subscribersList.Select(x => x.Value).OfType<IHandle<T>>().ToList())
        {
            handler.Handle(message);
        }

        var handlers = this._subscribersList
            .ToList()
            .Select(x => x.Value)
            .OfType<IHandleAsync<T>>()
            .Select(s => s.HandleAsync(message))
            .Where(t => t.Status != TaskStatus.RanToCompletion)
            .ToList();

        if (handlers.Any())
        {
            return Task.WhenAll(handlers);
        }

        return Task.CompletedTask;
    }
}