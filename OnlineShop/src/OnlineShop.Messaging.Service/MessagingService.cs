using System.Linq.Expressions;
using OnlineShop.Messaging.Abstraction;
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

    public void Publish<T>(T message)
    {
        _publisherStorage.Publish(message);
    }

    public void Subscribe<TEventArgs>(EventHandler<TEventArgs> handler)
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
            Queues = new List<string> { "CatalogService" },
        };
    }
}