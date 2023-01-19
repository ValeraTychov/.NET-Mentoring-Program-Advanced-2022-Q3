namespace OnlineShop.Messaging.Abstraction;

public interface ISubscriber<out TMessage> : IDisposable
{
    void Subscribe(Action<TMessage> handler);
}
