using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Channels;
using OnlineShop.Messaging.Service.EventArguments;
using OnlineShop.Messaging.Service.Models;
using OnlineShop.Messaging.Service.Storage;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OnlineShop.Messaging.Service.Tests
{
    [TestClass]
    public class ConnectionHandlerTests
    {
        private readonly Settings _settings = new Settings
        {
            Host = "localhost",
            Username = "planck",
            Password = "planck",
            Queues = new List<string> { "CatalogService" },
        };
        private PublisherStorage _publisherStorage = new PublisherStorage();
        private SubscriptionStorage _subscriptionStorage = new SubscriptionStorage();

        [TestMethod]
        public void Publish()
        {
            using var bh = new BusHandler(_settings, _publisherStorage, _subscriptionStorage);
            var message = new TestEventArgs { Id = 42, Name = "Test" };
            _publisherStorage.Publish(message);

        }

        [TestMethod]
        public void Subscribe()
        {
            using var bh = new BusHandler(_settings, _publisherStorage, _subscriptionStorage);
            var expected = new TestEventArgs { Id = 42, Name = "Test" };

            _subscriptionStorage.Subscribe<TestEventArgs>(OnMessageReceived);
            Thread.Sleep(10000);

            void OnMessageReceived(object o, TestEventArgs e)
            {
                Assert.AreEqual(expected.Id, e.Id);
            }
        }
    }
}