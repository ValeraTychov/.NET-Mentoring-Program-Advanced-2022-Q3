using AutoMapper;
using OnlineShop.CartService.BLL.Entities;
using OnlineShop.CartService.BLL.MappingProfiles;
using FluentAssertions;

namespace OnlineShop.CartService.BLL.Test;

[TestClass]
public class CartServiceProfileTests
{
    [TestMethod]
    public void MapFromBllToDalEntity_ValueIsNull_ShouldReturnNull()
    {
        Cart cart = null;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CartServiceProfile>());
        var actual = config.CreateMapper().Map<DAL.Entities.Cart>(cart);

        actual.Should().BeNull();
    }

    [TestMethod]
    public void MapFromBllToDalEntity_GivenCorrectValue_ShouldMapCorrectly()
    {
        var expextedUri = "https://example.com/foo.jpg";

        var cart = new Cart
        {
            Id = new Guid(),
            Items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new Image
                    {
                        Url = expextedUri,
                        AltText = "test",
                    },
                    Price = 0.0M,
                    Quantity = 1,
                }
            },
        };

        var expected = new DAL.Entities.Cart
        {
            Id = new Guid(),
            Items = new DAL.Entities.Item[]
            {
                new DAL.Entities.Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new DAL.Entities.Image
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
        var actual = config.CreateMapper().Map<DAL.Entities.Cart>(cart);
        
        actual.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void MapFromDalToBllEntity_GivenCorrectValue_ShouldMapCorrectly()
    {
        var expextedUri = "https://example.com/foo.jpg";

        var cart = new DAL.Entities.Cart
        {
            Id = new Guid(),
            Items = new DAL.Entities.Item[]
            {
                new DAL.Entities.Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new DAL.Entities.Image
                    {
                        Url = expextedUri,
                        AltText = "test",
                    },
                    Price = 0.0M,
                    Quantity = 1,
                }
            },
        };

        var expected = new Cart
        {
            Id = new Guid(),
            Items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new Image
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
        var actual = config.CreateMapper().Map<Cart>(cart);

        actual.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void AddCartIntoCollection()
    {
        var cart = new DAL.Entities.Cart
        {
            Id = new Guid(),
            Items = new DAL.Entities.Item[]
            {
                new DAL.Entities.Item
                {
                    Id = 1,
                    Name = "test",
                    Image = new DAL.Entities.Image
                    {
                        Url = "example.com",
                        AltText = "test",
                    },
                    Price = 0.0M,
                    Quantity = 1,
                }
            },
        };
        var repo = new DAL.CartRepository();
        repo.AddOrUpdate(cart);
    }
}