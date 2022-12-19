using AutoMapper;
using OnlineShop.CatalogService.Api.Entities;
using System.Runtime.CompilerServices;
using DomainCategory = OnlineShop.CatalogService.Domain.Entities.Category;
using DomainItem = OnlineShop.CatalogService.Domain.Entities.Item;

namespace OnlineShop.CatalogService.Api.MappingProfiles;

public class CatalogServiceProfile : Profile
{
    public CatalogServiceProfile()
    {
        CreateMap<Category, DomainCategory>().ForMember(di => di.Parent, cfg => cfg.MapFrom(x => new DomainCategory { Id = x.ParentId ?? 0 }));
        CreateMap<DomainCategory, Category>();
        CreateMap<Item, DomainItem>().ForMember(di => di.Category, cfg => cfg.MapFrom(x => new DomainCategory { Id = x.CategoryId }));
        CreateMap<DomainItem, Item>();
    }
}
