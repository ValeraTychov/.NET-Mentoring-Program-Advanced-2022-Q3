using AutoMapper;
using OnlineShop.CatalogService.WebApplication.Entities;
using DomainCategory = OnlineShop.CatalogService.Domain.Entities.Category;
using DomainItem = OnlineShop.CatalogService.Domain.Entities.Item;

namespace OnlineShop.CatalogService.WebApplication.MappingProfiles;

public class CatalogServiceProfile : Profile
{
    public CatalogServiceProfile()
    {
        CreateMap<Category, DomainCategory>().ReverseMap();
        CreateMap<Item, DomainItem>().ReverseMap();
    }
}
