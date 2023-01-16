using OnlineShop.Messaging.Service.Events;
using RabbitMQ.Client;

namespace OnlineShop.Messaging.Service.Models;

public interface IConnectionProvider
{
    IConnection Connection { get; }

    event EventHandler<ShutdownEventArgs>? ConnectionShutdown;
    
    event EventHandler<ConnectionCreatedEventArgs>? ConnectionCreated;
}
