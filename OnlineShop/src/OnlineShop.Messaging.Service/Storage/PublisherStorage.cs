using System.Collections.Concurrent;
using OnlineShop.Messaging.Service.Models;

namespace OnlineShop.Messaging.Service.Storage;

public class PublisherStorage
{
    private readonly ConcurrentQueue<Message> _messageBuffer = new();

    public EventHandler MessageReceived;

    public void Publish<T>(T data)
    {
        var message = new Message(data);
        _messageBuffer.Enqueue(message);
        MessageReceived?.Invoke(this, EventArgs.Empty);
    }

    public bool TryPeek(out Message message)
    {
        return _messageBuffer.TryPeek(out message);
    }

    public bool TryDequeue(out Message message)
    {
        return _messageBuffer.TryDequeue(out message);
    }
}