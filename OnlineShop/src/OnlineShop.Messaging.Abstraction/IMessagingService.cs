using OnlineShop.Messaging.Abstraction.Entities;

namespace OnlineShop.Messaging.Abstraction;

public interface IMessagingService
{
    public void Publish(EventParameters eventParameters);

    public void Subscribe<TEventParameters>(Action<TEventParameters> handler) where TEventParameters : EventParameters;
}