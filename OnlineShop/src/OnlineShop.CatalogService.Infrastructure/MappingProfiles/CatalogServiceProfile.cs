using AutoMapper;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.Messaging.Abstraction.Entities;
using DalCategory = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Category;
using DalItem = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Item;

namespace OnlineShop.CatalogService.Infrastructure.MappingProfiles;

public class CatalogServiceProfile : Profile
{
    public CatalogServiceProfile()
    {
        CreateMap<Category,DalCategory>().ReverseMap();
        CreateMap<Item, DalItem>().ReverseMap();

        CreateMap<Item, ItemChangedParameters>();
    }
}
