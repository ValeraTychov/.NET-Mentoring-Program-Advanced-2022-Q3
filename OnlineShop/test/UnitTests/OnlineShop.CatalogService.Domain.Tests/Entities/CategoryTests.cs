using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain.Tests.Entities;

[TestClass]
public class CategoryTests
{
    [TestMethod]
    public void Validate_NameIsNull_ShouldBeInvalid()
    {
        var expectedFailMessage = "Name property is required";
        var category = new Category();
        var result = category.Validate();
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(expectedFailMessage, result.Message);
    }
}
