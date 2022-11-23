using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OnlineShop.Messaging.Service.Models;

public class ConnectionContext : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private EventingBasicConsumer? _consumer;

    public ConnectionContext(IConnectionFactory connectionFactory, List<string>? queues = null)
    {
        _connection = connectionFactory.CreateConnection();
        _connection.ConnectionShutdown += OnConnectionShutdown;
        _channel = _connection.CreateModel();

        if (queues == null || !queues.Any())
        {
            return;
        }

        SetupListener(queues);
    }

    public void SetupListener(List<string> queues)
    {
        _consumer = new EventingBasicConsumer(_channel);
        DeclareQueues(queues);
        SetupConsumer(queues);
    }

    public event EventHandler<BasicDeliverEventArgs>? EventReceived;
    public event EventHandler<ShutdownEventArgs>? ConnectionShutdown;

    public IModel GetChannel(string queue)
    {
        _channel.QueueDeclare(queue, false, false, false, null);
        return _channel;
    }

    public void AckReceipt(ulong deliveryTag)
    {
        _channel.BasicAck(deliveryTag, false);
    }

    private void DeclareQueues(List<string> queues)
    {
        queues.ForEach(queue => _channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null));
    }

    private void SetupConsumer(List<string> queues)
    {
        queues.ForEach(queue => _channel.BasicConsume(
            queue: queue,
            autoAck: false,
            consumer: _consumer));

        _consumer.Received += OnConsumerReceived;
    }

    private void OnConsumerReceived(object? sender, BasicDeliverEventArgs args)
    {
        EventReceived?.Invoke(sender, args);
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs args)
    {
        ConnectionShutdown?.Invoke(sender, args);
    }
    
    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}