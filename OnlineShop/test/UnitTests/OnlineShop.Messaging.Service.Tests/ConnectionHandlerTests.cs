using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;
using OnlineShop.Messaging.Service.Models;
using OnlineShop.Messaging.Service.Storage;

namespace OnlineShop.Messaging.Service.Tests
{
    [TestClass]
    public class ConnectionHandlerTests
    {
        private readonly MessageBrokerSettings _settings = new MessageBrokerSettings
        {
            Host = "localhost",
            Username = "planck",
            Password = "planck",
        };
        //private PublisherStorage _publisherStorage = new PublisherStorage();
        //private SubscriptionStorage _subscriptionStorage = new SubscriptionStorage();

        [TestMethod]
        public void Publish()
        {
            var connectionProvider = new ConnectionProvider(_settings);
            using var publisher = new Publisher<TestMessage>(connectionProvider);

            //using var bh = new BusHandler(_settings, _publisherStorage, _subscriptionStorage);
            var message = new TestMessage { Id = 42, Name = "Test" };
            publisher.Publish(message);
        }

        [TestMethod]
        public void Subscribe()
        {
            EventWaitHandle waitHandle = new ManualResetEvent(false);
            var connectionProvider = new ConnectionProvider(_settings);
            using var subscriber = new Subscriber<TestMessage>(connectionProvider);
            TestMessage result = null;

            subscriber.Subscribe(OnMessageReceived);

            waitHandle.WaitOne(2000);
            Assert.IsNotNull(result);

            void OnMessageReceived(TestMessage parameters)
            {
                result = parameters;
                waitHandle.Set();
            }
        }

        private class TestMessage
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}