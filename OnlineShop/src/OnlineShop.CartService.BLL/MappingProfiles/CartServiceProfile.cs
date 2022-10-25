using AutoMapper;
using OnlineShop.CartService.BLL.Entities;

namespace OnlineShop.CartService.BLL.MappingProfiles;

public class CartServiceProfile : Profile
{
	public CartServiceProfile()
	{
		CreateMap<Cart, OnlineShop.CartService.DAL.Entities.Cart>().ReverseMap();
        CreateMap<Item, OnlineShop.CartService.DAL.Entities.Item>().ReverseMap();
        CreateMap<Image, OnlineShop.CartService.DAL.Entities.Image>().ReverseMap();
    }
}
