using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Service.Models;
using OnlineShop.Messaging.Service.Storage;

namespace OnlineShop.Messaging.Service;

public class Publisher<TMessage> : IPublisher<TMessage>, IDisposable
{
    private readonly PublisherStorage<TMessage> _publisherStorage;
    private readonly PublishManager<TMessage> _publishManager;

    public Publisher(IConnectionProvider connectionProvider)
    {
        _publisherStorage = new PublisherStorage<TMessage>();
        _publishManager = new PublishManager<TMessage>(_publisherStorage, connectionProvider);
    }

    public void Publish(TMessage message)
    {
        _publisherStorage.Publish(message);
    }

    public void Dispose()
    {
        _publishManager.Dispose();
    }


}
