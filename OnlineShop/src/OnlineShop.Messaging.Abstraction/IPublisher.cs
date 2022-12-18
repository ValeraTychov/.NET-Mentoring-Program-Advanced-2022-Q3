namespace OnlineShop.Messaging.Abstraction
{
    public interface IPublisher<TMessage>
    {
        public void Publish(TMessage message);
    }
}
