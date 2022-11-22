using Moq;
using AutoMapper;
using OnlineShop.CartService.DAL;
using OnlineShop.CartService.BLL.Entities;
using OnlineShop.Messaging.Abstraction;

namespace OnlineShop.CartService.BLL.Test;

[TestClass]
public class CartServiceTests
{
    [TestMethod]
    public void GetItems()
    {
        var repository = new Mock<ICartRepository>();
        var mapper = new Mock<IMapper>();
        var messagingService = new Mock<IMessagingService>();
        var service = new CartService(mapper.Object, repository.Object, messagingService.Object);
        var guid = Guid.NewGuid();
        
        service.GetItems(guid);

        mapper.Verify(m => m.Map<Cart>(It.IsAny<Cart>()));
        repository.Verify(r => r.Get(It.Is<Guid>(v => v == guid)));
    }

    [TestMethod]
    public void AddItem_NoCartInDb_ShouldCreateNewAndAddItem()
    {
        var repository = new Mock<ICartRepository>();
        var mapper = new Mock<IMapper>();
        var messagingService = new Mock<IMessagingService>();
        mapper.Setup(m => m.Map<Cart>(It.IsAny<DAL.Entities.Cart>())).Returns<Cart>(null);
        var item = Mock.Of<Item>();
        var service = new CartService(mapper.Object, repository.Object, messagingService.Object);
        
        service.AddItem(Guid.NewGuid(), item);

        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.IsNotNull<Cart>()));
        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.Is<Cart>(c => c.Items.Contains(item))));
    }

    [TestMethod]
    public void AddItem_NoItemInCart_ShouldChangeQuantityCorrectly()
    {
        var repository = new Mock<ICartRepository>();
        var mapper = new Mock<IMapper>();
        var messagingService = new Mock<IMessagingService>();
        Cart? cart = null;
        mapper.Setup(m => m.Map<Cart?>(It.IsAny<DAL.Entities.Cart>())).Returns(cart);
        var item = new Item { Quantity = 42 };
        var service = new CartService(mapper.Object, repository.Object, messagingService.Object);
        
        service.AddItem(Guid.NewGuid(), item);

        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.Is<Cart>(c => c.Items[0].Quantity == 42)));
    }

    [TestMethod]
    public void AddItem_CartAlreadyContainItem_ShouldChangeQuantityCorrectly()
    {
        var itemInCart = new Item { Quantity = 2 };
        var repository = new Mock<ICartRepository>();
        var messagingService = new Mock<IMessagingService>();
        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<Cart>(It.IsAny<DAL.Entities.Cart>())).Returns(new Cart { Items = new List<Item> { itemInCart } });
        var itemToAdd = new Item { Quantity = 2 };
        var service = new CartService(mapper.Object, repository.Object, messagingService.Object);
        
        service.AddItem(Guid.NewGuid(), itemToAdd);

        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.Is<Cart>(c => c.Items[0].Quantity == 4)));
    }
}
