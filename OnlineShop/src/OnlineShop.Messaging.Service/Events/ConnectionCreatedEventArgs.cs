using RabbitMQ.Client;

namespace OnlineShop.Messaging.Service.Events;

public class ConnectionCreatedEventArgs : EventArgs
{
    public IConnection Connection { get; set; }
}
