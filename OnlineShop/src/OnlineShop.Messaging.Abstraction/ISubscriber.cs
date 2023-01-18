namespace OnlineShop.Messaging.Abstraction;

public interface ISubscriber<TMessage> : IDisposable
{
    void Subscribe(Action<TMessage> handler);
}
