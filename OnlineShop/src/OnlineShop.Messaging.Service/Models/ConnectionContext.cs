using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OnlineShop.Messaging.Service.Models;

public class ConnectionContext : IDisposable
{
    public ConnectionContext(IConnectionFactory connectionFactory, List<string> queues)
    {
        Connection = connectionFactory.CreateConnection();
        Channel = Connection.CreateModel();
        DeclareQueues(queues);
        SetupConsumer(queues);
    }

    public IConnection Connection { get; init; }

    public IModel Channel { get; init; }

    public EventingBasicConsumer Consumer { get; private set; }

    private void DeclareQueues(List<string> queues)
    {
        queues.ForEach(queue => Channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null));
    }

    private void SetupConsumer(List<string> queues)
    {
        Consumer = new EventingBasicConsumer(Channel);
        
        queues.ForEach(queue => Channel.BasicConsume(
            queue: queue,
            autoAck: false,
            consumer: Consumer));
    }
    
    public void Dispose()
    {
        Channel?.Dispose();
        Connection?.Dispose();
    }
}