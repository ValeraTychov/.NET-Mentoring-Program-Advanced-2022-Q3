using System.Text;
using Newtonsoft.Json;
using OnlineShop.Messaging.Service.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OnlineShop.Messaging.Service.Models;

internal class ListenManager<TMessage> : IDisposable
{
    private readonly IConnectionProvider _connectionProvider;
    private Action<TMessage> _onMessage;

    private IConnection _connection;
    private IModel _channel;
    private EventingBasicConsumer? _consumer;

    public ListenManager(IConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
        _connectionProvider.ConnectionCreated += OnConnectionCreated;
        _connection = connectionProvider.Connection;

        SetupListener();
    }

    public void Subscribe(Action<TMessage> handler)
    {
        _onMessage += handler;
    }

    private void OnConnectionCreated(object? sender, ConnectionCreatedEventArgs e)
    {
        _connection = e.Connection;

        SetupListener();
    }

    private void SetupListener()
    {
        _channel = _connection.CreateModel();
        _consumer = new EventingBasicConsumer(_channel);
        var queue = typeof(TMessage).Name;
        DeclareQueue(queue);
        SetupConsumer(queue);
    }

    private void DeclareQueue(string queue)
    {
        _channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    private void SetupConsumer(string queue)
    {
        _channel.BasicConsume(
            queue: queue,
            autoAck: false,
            consumer: _consumer);

        _consumer.Received += OnConsumerReceived;
    }

    private void OnConsumerReceived(object? sender, BasicDeliverEventArgs e)
    {
        string body = Encoding.UTF8.GetString(e.Body.ToArray());
        var message = JsonConvert.DeserializeObject<TMessage>(body);

        _onMessage(message);

        AckReceipt(e.DeliveryTag);
    }

    public void AckReceipt(ulong deliveryTag)
    {
        _channel.BasicAck(deliveryTag, false);
    }

    public void Dispose()
    {
        _onMessage = null;
        _connectionProvider.ConnectionCreated -= OnConnectionCreated;
        _channel.Dispose();
        _connection.Dispose();
    }
}
