using System.Collections.Concurrent;
using OnlineShop.Messaging.Abstraction.Entities;

namespace OnlineShop.Messaging.Service.Storage;

public class PublisherStorage
{
    private readonly ConcurrentQueue<EventParameters> _messageBuffer = new();

    public EventHandler MessageReceived;

    public void Publish(EventParameters eventParameters)
    {
        _messageBuffer.Enqueue(eventParameters);
        MessageReceived?.Invoke(this, EventArgs.Empty);
    }

    public bool TryPeek(out EventParameters eventParameters)
    {
        return _messageBuffer.TryPeek(out eventParameters);
    }

    public bool TryDequeue(out EventParameters eventParameters)
    {
        return _messageBuffer.TryDequeue(out eventParameters);
    }
}