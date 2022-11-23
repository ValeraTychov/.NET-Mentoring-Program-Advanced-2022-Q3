using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;
using OnlineShop.Messaging.Service.Storage;

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
            QueuesToListen = new List<Type> { typeof(TestEventParameters), typeof(ItemChangedParameters) },
        };
        private PublisherStorage _publisherStorage = new PublisherStorage();
        private SubscriptionStorage _subscriptionStorage = new SubscriptionStorage();

        [TestMethod]
        public void Publish()
        {
            using var bh = new BusHandler(_settings, _publisherStorage, _subscriptionStorage);
            var message = new TestEventParameters { Id = 42, Name = "Test" };
            _publisherStorage.Publish(message);
        }

        [TestMethod]
        public void Subscribe()
        {
            EventWaitHandle waitHandle = new ManualResetEvent(false);
            using var bh = new BusHandler(_settings, _publisherStorage, _subscriptionStorage);
            var expected = new TestEventParameters { Id = 42, Name = "Test" };

            _subscriptionStorage.Subscribe<TestEventParameters>(OnMessageReceived);

            TestEventParameters? result = null;

            waitHandle.WaitOne(5000);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Id, result.Id);

            void OnMessageReceived(TestEventParameters parameters)
            {
                result = parameters;
                waitHandle.Set();
            }
        }

        private class TestEventParameters : EventParameters
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}