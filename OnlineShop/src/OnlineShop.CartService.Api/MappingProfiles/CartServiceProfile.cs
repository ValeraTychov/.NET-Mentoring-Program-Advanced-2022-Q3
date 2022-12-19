using AutoMapper;
using OnlineShop.CartService.Api.Entities;

namespace OnlineShop.CartService.Api.MappingProfiles;

public class CartServiceProfile : Profile
{
	public CartServiceProfile()
	{
		CreateMap<Cart, OnlineShop.CartService.BLL.Entities.Cart>().ReverseMap();
        CreateMap<Item, OnlineShop.CartService.BLL.Entities.Item>().ReverseMap();
        CreateMap<Image, OnlineShop.CartService.BLL.Entities.Image>().ReverseMap();
    }
}
