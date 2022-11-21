namespace OnlineShop.Messaging.Abstraction;

public interface IMessagingService
{
    public void Publish<T>(T message);

    public void Subscribe<TEventArgs>(EventHandler<TEventArgs> handler);

}