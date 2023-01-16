using AutoMapper;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Infrastructure.DAL;
using OnlineShop.CatalogService.Infrastructure.MappingProfiles;

namespace OnlineShop.CatalogService.Domain.Tests;

[TestClass]
public class CategoryServiceTests
{
    private ICategoryService _categoryService;

    public CategoryServiceTests()
    {
        var dbContext = new CatalogContext();
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CatalogServiceProfile>()).CreateMapper();
        var categoryRepository = new CategoryRepository(mapper, dbContext);
        _categoryService = new CategoryService(categoryRepository);
    }

    [TestMethod]
    public void GetCategoryWithParent()
    {
        var category = _categoryService.Get(13);
        Assert.IsNotNull(category);
    }

    [TestMethod]
    public void AddCategoryWithParent()
    {
        _categoryService.Add(new Category { Name = "QQQ", Parent = new Category { Name = "Test Parent", Id = 99 } });
    }

    [TestMethod]
    public void UpdateCategoryWithParent()
    {
        _categoryService.Update(new Category { Id = 13, Name = "Foo", Parent = new Category { Name = "Bar", Id = 6 } });
    }
}
