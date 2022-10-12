using AutoMapper;
using OnlineShop.BLL.CartService.Entities;
using OnlineShop.BLL.CartService.MappingProfiles;
using FluentAssertions;

namespace BLL.CartService.Test;

[TestClass]
public class MappingProfileTests
{
    [TestMethod]
    public void MapperProfileTest()
    {
        var expextedUri = "https://example.com/foo.jpg";

        var cart = new Cart
        {
            Id = new Guid(),
            Items = new Item[]
            {
                new Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new Image
                    {
                        Url = new Uri(expextedUri),
                        AltText = "test",
                    },
                    Price = 0.0M,
                    Quantity = 1,
                }
            },
        };

        var expected = new OnlineShop.DAL.CartService.Entities.Cart
        {
            Id = new Guid(),
            Items = new OnlineShop.DAL.CartService.Entities.Item[]
            {
                new OnlineShop.DAL.CartService.Entities.Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new OnlineShop.DAL.CartService.Entities.Image
                    {
                        Url = expextedUri,
                        AltText = "test",
                    },
                    Price = 0.0M,
                    Quantity = 1,
                }
            },
        };

        var config = new MapperConfiguration(cfg => cfg.AddProfile<CartServiceProfile>());
        var actual = config.CreateMapper().Map<OnlineShop.DAL.CartService.Entities.Cart>(cart);
        
        actual.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void AddCartIntoCollection()
    {
        var cart = new OnlineShop.DAL.CartService.Entities.Cart
        {
            Id = new Guid(),
            Items = new OnlineShop.DAL.CartService.Entities.Item[]
            {
                new OnlineShop.DAL.CartService.Entities.Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new OnlineShop.DAL.CartService.Entities.Image
                    {
                        Url = "example.com",
                        AltText = "test",
                    },
                    Price = 0.0M,
                    Quantity = 1,
                }
            },
        };
        var repo = new OnlineShop.DAL.CartService.CartRepository();
        repo.AddOrUpdate(cart);
    }
}