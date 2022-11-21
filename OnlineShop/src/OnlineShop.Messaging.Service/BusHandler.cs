using System.Reflection;
using OnlineShop.Messaging.Service.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using OnlineShop.Messaging.Service.Storage;
using System.Text;

namespace OnlineShop.Messaging.Service;

public class BusHandler : IDisposable
{
    private readonly PublisherStorage _publisherStorage;
    private readonly SubscriptionStorage _subscriptionStorage;
    private readonly IConnectionFactory _connectionFactory;
    private readonly List<string> _queues;
    private ConnectionContext _context;

    private int _locked;

    public BusHandler(Settings settings, PublisherStorage publisherStorage, SubscriptionStorage subscriptionStorage)
    {
        _publisherStorage = publisherStorage;
        _publisherStorage.MessageReceived += PublishMessages;
        _subscriptionStorage = subscriptionStorage;
        _connectionFactory = new ConnectionFactory
        {
            HostName = settings.Host,
            UserName = settings.Username,
            Password = settings.Password,
        };

        _queues = settings.Queues;
        _context = CreateConnectionContext(_connectionFactory, _queues);
        SetupContext();
        PublishMessages(this, EventArgs.Empty);
    }
    
    private ConnectionContext CreateConnectionContext(IConnectionFactory connectionFactory, List<string> queues)
    {
        while (true)
        {
            try
            {
                return new ConnectionContext(connectionFactory, queues);
            }
            catch (Exception ex)
            {
                //Good place for logger
                Thread.Sleep(2000);
            }
        }
    }

    private void SetupContext()
    {
        _context.Connection.ConnectionShutdown += OnConnectionShutdown;
        _context.Consumer.Received += OnConsumerReceived;
    }

    private void PublishMessages(object sender, EventArgs args)
    {
        if (Interlocked.CompareExchange(ref _locked, 1, 0) != 0)
        {
            return;
        }

        while (_publisherStorage.TryPeek(out var message))
        {
            try
            {
                PublishMessage(message);
            }
            catch
            {
                break;
            }
            
            Dequeue();
        }

        _locked = 0;
    }

    private void Dequeue()
    {
        while (!_publisherStorage.TryDequeue(out var _))
        {
        }
    }

    private void PublishMessage(Message message)
    {
        var properties = _context.Channel.CreateBasicProperties();
        properties.Headers = new Dictionary<string, object>();
        properties.Headers.Add(new KeyValuePair<string, object>("type", message.Type));

        var str = JsonConvert.SerializeObject(message.Data);
        var body = Encoding.UTF8.GetBytes(str);
        _context.Channel.BasicPublish(string.Empty, "CatalogService", true, properties, body);
    }

    private void OnConsumerReceived(object sender, BasicDeliverEventArgs args)
    {
        var type = Type.GetType(Encoding.UTF8.GetString((byte[])args.BasicProperties.Headers["type"]));
        string body = Encoding.UTF8.GetString(args.Body.ToArray());
        dynamic eventArgs = JsonConvert.DeserializeObject(body, type);

        if (_subscriptionStorage.TryInvoke(eventArgs))
        {
            _context.Channel.BasicAck(args.DeliveryTag, false);
        }
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs eventArgs)
    {
        _context.Dispose();
        _context = CreateConnectionContext(_connectionFactory, _queues);
        SetupContext();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}