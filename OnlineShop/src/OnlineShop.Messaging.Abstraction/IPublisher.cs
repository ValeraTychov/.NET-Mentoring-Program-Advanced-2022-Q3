namespace OnlineShop.Messaging.Abstraction
{
    public interface IPublisher<in TMessage>
    {
        public void Publish(TMessage message);
    }
}
