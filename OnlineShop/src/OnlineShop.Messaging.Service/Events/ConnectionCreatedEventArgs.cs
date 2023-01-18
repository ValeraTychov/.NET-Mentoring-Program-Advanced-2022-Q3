using RabbitMQ.Client;

namespace OnlineShop.Messaging.Service.Events;

public class ConnectionCreatedEventArgs : EventArgs
{
    public ConnectionCreatedEventArgs(IConnection connection)
    {
        Connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public IConnection Connection { get; private set; }
}
