using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Service.Events;
using RabbitMQ.Client;

namespace OnlineShop.Messaging.Service.Models;

public class ConnectionProvider : IConnectionProvider, IDisposable
{
    private IConnection _connection;
    private readonly IConnectionFactory _connectionFactory;

    public ConnectionProvider(MessageBrokerSettings settings)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = settings.Host,
            UserName = settings.Username,
            Password = settings.Password,
        };

        _connection = CreateConnection();
    }

    public IConnection Connection => _connection;

    public event EventHandler<ShutdownEventArgs>? ConnectionShutdown;
    public event EventHandler<ConnectionCreatedEventArgs>? ConnectionCreated;

    private IConnection CreateConnection()
    {
        var maxAttempts = 1000;
        var attempt = 0;

        while (true)
        {
            try
            {
                return _connectionFactory.CreateConnection();
            }
            catch (Exception ex)
            {
                if(++attempt >= maxAttempts)
                {
                    throw new Exception("Cannot create connextion", ex);
                }
                
                Thread.Sleep(2000);
            }
        }
    }

    private void SetupConnection(IConnection connection)
    {
        connection.ConnectionShutdown += OnConnectionShutdown;
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs args)
    {
        Dispose();
        ConnectionShutdown?.Invoke(sender, args);
        _connection = CreateConnection();
        SetupConnection(_connection);
        ConnectionCreated?.Invoke(this, new ConnectionCreatedEventArgs { Connection = _connection });
    }

    public void Dispose()
    {
        _connection.ConnectionShutdown -= OnConnectionShutdown;
        _connection.Dispose();
    }
}
