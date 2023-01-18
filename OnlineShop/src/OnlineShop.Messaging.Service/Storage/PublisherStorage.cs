using System.Collections.Concurrent;

namespace OnlineShop.Messaging.Service.Storage;

public class PublisherStorage<TMessage>
{
    private readonly ConcurrentQueue<TMessage> _messageBuffer = new();

    public EventHandler? MessageReceived;

    public void Publish(TMessage message)
    {
        _messageBuffer.Enqueue(message);
        MessageReceived?.Invoke(this, EventArgs.Empty);
    }

    public bool TryPeek(out TMessage? message)
    {
        return _messageBuffer.TryPeek(out message);
    }

    public bool TryDequeue(out TMessage? message)
    {
        return _messageBuffer.TryDequeue(out message);
    }
}
