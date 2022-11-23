using OnlineShop.Messaging.Service.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using OnlineShop.Messaging.Service.Storage;
using System.Text;
using OnlineShop.Messaging.Abstraction.Entities;

namespace OnlineShop.Messaging.Service;

public class BusHandler : IDisposable
{
    private readonly PublisherStorage _publisherStorage;
    private readonly SubscriptionStorage _subscriptionStorage;
    private readonly IConnectionFactory _connectionFactory;
    private readonly Dictionary<string, Type> _queueMap;
    private ConnectionContext _context;

    private int _locked;

    public BusHandler(Settings settings, PublisherStorage publisherStorage, SubscriptionStorage subscriptionStorage)
    {
        _publisherStorage = publisherStorage;
        _publisherStorage.MessageReceived += PublishMessages;
        _subscriptionStorage = subscriptionStorage;
        _queueMap = CreateQueueMap(settings.Queues);
        _connectionFactory = CreateConnectionFactory(settings);
        
        _context = CreateConnectionContext(_connectionFactory, _queueMap.Keys.ToList());
        SetupContext();
        PublishMessages(this, EventArgs.Empty);
    }

    private IConnectionFactory CreateConnectionFactory(Settings settings)
    {
        return new ConnectionFactory
        {
            HostName = settings.Host,
            UserName = settings.Username,
            Password = settings.Password,
        };
    }

    private Dictionary<string, Type> CreateQueueMap(List<Type> queues)
    {
        return queues.ToDictionary(x => x.Name, x => x);
    }
    
    private ConnectionContext CreateConnectionContext(IConnectionFactory connectionFactory, List<string> queues)
    {
        while (true)
        {
            try
            {
                return new ConnectionContext(connectionFactory, queues);
            }
            catch (Exception)
            {
                //Good place for logger
                Thread.Sleep(2000);
            }
        }
    }

    private void SetupContext()
    {
        _context.ConnectionShutdown += OnConnectionShutdown;
        _context.EventReceived += OnConsumerReceived;
    }

    private void PublishMessages(object? sender, EventArgs args)
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

    private void PublishMessage(EventParameters eventParameters)
    {
        var str = JsonConvert.SerializeObject(eventParameters);
        var body = Encoding.UTF8.GetBytes(str);
        var queue = eventParameters.GetType().Name;

        using var channel = _context.CreateChannel(queue);
        channel.BasicPublish(string.Empty, queue, true, null, body);
    }

    private void OnConsumerReceived(object? sender, BasicDeliverEventArgs args)
    {
        var type = _queueMap[args.RoutingKey];
        string body = Encoding.UTF8.GetString(args.Body.ToArray());
        var eventParameters = JsonConvert.DeserializeObject(body, type);
        
        if (TryNotifySubscribers(type, eventParameters))
        {
            _context.AckReceipt(args.DeliveryTag);
        }
    }

    private bool TryNotifySubscribers(Type type, object? eventParameters)
    {
        return (bool)(typeof(SubscriptionStorage)
            .GetMethod(nameof(_subscriptionStorage.TryNotifySubscribers))
            ?.MakeGenericMethod(type)
            .Invoke(_subscriptionStorage, new[] { eventParameters }) ?? false);
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs eventArgs)
    {
        _context.Dispose();
        _context = CreateConnectionContext(_connectionFactory, _queueMap.Keys.ToList());
        SetupContext();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}