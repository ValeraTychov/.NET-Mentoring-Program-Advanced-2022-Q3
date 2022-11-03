using AutoMapper;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Infrastructure.DAL;
using OnlineShop.CatalogService.Infrastructure.MappingProfiles;

using DalCategory = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Category;

namespace OnlineShop.CatalogService.Domain.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dbContext = new CatalogContext();
            var mapper = 
                new MapperConfiguration(cfg => cfg.AddProfile<CatalogServiceProfile>())
                .CreateMapper();
            var categoryRepository = new GenericRepository<Category, DalCategory>(mapper, dbContext);
            var categoryService = new CatalogService(categoryRepository);
            categoryService.Add(new Category { Name = "Category 2", Parent = new Category { Id = 8 } } );
        }
    }
}