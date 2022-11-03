using AutoMapper;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Infrastructure.DAL;
using OnlineShop.CatalogService.Infrastructure.MappingProfiles;
using DalItem = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Item;
using DalCategory = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Category;

namespace OnlineShop.CatalogService.WebApplication.DI
{
    public class CatalogServiceFactory
    {
        public static Domain.CatalogService Create()
        {
            var dbContext = new CatalogContext();
            var mapper =
                new MapperConfiguration(cfg => cfg.AddProfile<CatalogServiceProfile>())
                .CreateMapper();

            var itemRepository = new GenericRepository<Item, DalItem>(mapper, dbContext);
            var categoryRepository = new GenericRepository<Category, DalCategory>(mapper, dbContext);

            var catalogService = new Domain.CatalogService(itemRepository, categoryRepository);

            return catalogService;
        }
    }
}
