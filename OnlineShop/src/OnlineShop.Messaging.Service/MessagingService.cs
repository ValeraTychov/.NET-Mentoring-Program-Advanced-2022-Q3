using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;
using OnlineShop.Messaging.Service.Models;
using OnlineShop.Messaging.Service.Storage;

namespace OnlineShop.Messaging.Service;

public class MessagingService : IMessagingService
{
    private readonly PublisherStorage _publisherStorage;
    private readonly SubscriptionStorage _subscriptionStorage;
    private readonly BusHandler _busHandler;

    public MessagingService()
    {
        var settings = LoadSettingsFromConfig();
        _publisherStorage = new PublisherStorage();
        _subscriptionStorage = new SubscriptionStorage();
        _busHandler = new BusHandler(settings, _publisherStorage, _subscriptionStorage);
    }

    public void Publish(EventParameters eventParameters)
    {
        _publisherStorage.Publish(eventParameters);
    }

    public void Subscribe<TEventParameters>(Action<TEventParameters> handler) where TEventParameters : EventParameters
    {
        _subscriptionStorage.Subscribe(handler);
    }

    private Settings LoadSettingsFromConfig()
    {
        return new Settings
        {
            Host = "localhost",
            Username = "planck",
            Password = "planck",
            Queues = new List<Type> { typeof(ItemChangedParameters) },
        };
    }
}