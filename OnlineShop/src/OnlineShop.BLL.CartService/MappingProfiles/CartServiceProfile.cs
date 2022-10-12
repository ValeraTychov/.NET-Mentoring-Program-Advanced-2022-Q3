using AutoMapper;
using OnlineShop.BLL.CartService.Entities;

namespace OnlineShop.BLL.CartService.MappingProfiles;

public class CartServiceProfile : Profile
{
	public CartServiceProfile()
	{
		CreateMap<Cart, DAL.CartService.Entities.Cart>();
        CreateMap<Item, DAL.CartService.Entities.Item>();
        CreateMap<Image, DAL.CartService.Entities.Image>();
    }
}
