using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Service.Models;

namespace OnlineShop.Messaging.Service;

public class Subscriber<TMessage> : ISubscriber<TMessage>
{
    private readonly ListenManager<TMessage> _listenManager;

    public Subscriber(IConnectionProvider connectionProvider)
    {
        _listenManager = new ListenManager<TMessage>(connectionProvider);
    }

    public void Subscribe(Action<TMessage> handler)
    {
        _listenManager.Subscribe(handler);
    }

    public void Dispose()
    {
        _listenManager.Dispose();
    }
}
