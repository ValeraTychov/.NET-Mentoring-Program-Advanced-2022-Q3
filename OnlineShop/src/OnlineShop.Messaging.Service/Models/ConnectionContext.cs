using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OnlineShop.Messaging.Service.Models;

public class ConnectionContext : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _listenerChannel;
    private readonly EventingBasicConsumer _consumer;

    public ConnectionContext(IConnectionFactory connectionFactory, List<string> queues)
    {
        _connection = connectionFactory.CreateConnection();
        _connection.ConnectionShutdown += OnConnectionShutdown;
        _listenerChannel = _connection.CreateModel();
        _consumer = new EventingBasicConsumer(_listenerChannel);
        DeclareQueues(queues);
        SetupConsumer(queues);
    }

    public event EventHandler<BasicDeliverEventArgs>? EventReceived;
    public event EventHandler<ShutdownEventArgs>? ConnectionShutdown;

    public IModel CreateChannel(string queue)
    {
        var channel = _connection.CreateModel();
        channel.QueueDeclare(queue, false, false, false, null);
        return channel;
    }

    public void AckReceipt(ulong deliveryTag)
    {
        _listenerChannel.BasicAck(deliveryTag, false);
    }

    private void DeclareQueues(List<string> queues)
    {
        queues.ForEach(queue => _listenerChannel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null));
    }

    private void SetupConsumer(List<string> queues)
    {
        queues.ForEach(queue => _listenerChannel.BasicConsume(
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
        _listenerChannel?.Dispose();
        _connection?.Dispose();
    }
}