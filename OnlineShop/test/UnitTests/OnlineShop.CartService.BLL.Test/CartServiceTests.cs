﻿using AutoMapper;
using Moq;
using OnlineShop.CartService.BLL.Entities;
using OnlineShop.CartService.DAL;
using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;

namespace OnlineShop.CartService.BLL.Test;

[TestClass]
public class CartServiceTests
{
    [TestMethod]
    public void GetItems()
    {
        var repository = new Mock<ICartRepository>();
        var mapper = new Mock<IMapper>();
        var subscriber = new Mock<ISubscriber<ItemChangedMessage>>();
        var service = new CartService(mapper.Object, repository.Object, subscriber.Object);
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
        var subscriber = new Mock<ISubscriber<ItemChangedMessage>>();
        mapper.Setup(m => m.Map<Cart>(It.IsAny<DAL.Entities.Cart>())).Returns<Cart>(null);
        var item = Mock.Of<Item>();
        var service = new CartService(mapper.Object, repository.Object, subscriber.Object);

        service.AddItem(Guid.NewGuid(), item);

        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.IsNotNull<Cart>()));
        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.Is<Cart>(c => c.Items.Contains(item))));
    }

    [TestMethod]
    public void AddItem_NoItemInCart_ShouldChangeQuantityCorrectly()
    {
        var repository = new Mock<ICartRepository>();
        var mapper = new Mock<IMapper>();
        var subscriber = new Mock<ISubscriber<ItemChangedMessage>>();
        Cart cart = null;
        mapper.Setup(m => m.Map<Cart>(It.IsAny<DAL.Entities.Cart>())).Returns(cart);
        var item = new Item { Quantity = 42 };
        var service = new CartService(mapper.Object, repository.Object, subscriber.Object);

        service.AddItem(Guid.NewGuid(), item);

        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.Is<Cart>(c => c.Items[0].Quantity == 42)));
    }

    [TestMethod]
    public void AddItem_CartAlreadyContainItem_ShouldChangeQuantityCorrectly()
    {
        var itemInCart = new Item { Quantity = 2 };
        var repository = new Mock<ICartRepository>();
        var subscriber = new Mock<ISubscriber<ItemChangedMessage>>();
        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<Cart>(It.IsAny<DAL.Entities.Cart>())).Returns(new Cart { Items = new List<Item> { itemInCart } });
        var itemToAdd = new Item { Quantity = 2 };
        var service = new CartService(mapper.Object, repository.Object, subscriber.Object);

        service.AddItem(Guid.NewGuid(), itemToAdd);

        mapper.Verify(m => m.Map<DAL.Entities.Cart>(It.Is<Cart>(c => c.Items[0].Quantity == 4)));
    }
}
