using AutoMapper;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Infrastructure.DAL;
using OnlineShop.CatalogService.Infrastructure.MappingProfiles;

namespace OnlineShop.CatalogService.Domain.Tests
{
    [TestClass]
    public class CatalogServiceTests 
    {
        private CatalogService _catalogService;

        public CatalogServiceTests()
        {
            var dbContext = new CatalogContext();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CatalogServiceProfile>()).CreateMapper();
            var categoryRepository = new CategoryRepository(mapper, dbContext);
            _catalogService = new CatalogService(categoryRepository);
        }

        [TestMethod]
        public void GetCategoryWithParent()
        {
            var category = _catalogService.Get<Category>(13);
            Assert.IsNotNull(category);
        }

        [TestMethod]
        public void AddCategoryWithParent()
        {
            _catalogService.Add(new Category { Name = "QQQ", Parent = new Category { Name = "Test Parent", Id = 99 } } );
        }

        [TestMethod]
        public void UpdateCategoryWithParent()
        {
            _catalogService.Update(new Category { Id = 13, Name = "Foo", Parent = new Category { Name = "Bar", Id = 6 } });
        }
    }
}