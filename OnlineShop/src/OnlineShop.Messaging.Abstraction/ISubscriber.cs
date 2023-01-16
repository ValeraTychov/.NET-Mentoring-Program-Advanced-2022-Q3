namespace OnlineShop.Messaging.Abstraction;

public interface ISubscriber<TMessage>
{
    void Subscribe(Action<TMessage> handler);
}
