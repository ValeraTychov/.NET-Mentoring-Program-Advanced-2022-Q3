using OnlineShop.CatalogService.Domain.Validation;

namespace OnlineShop.CatalogService.Domain.Tests.Validation;

[TestClass]
public class ValidatorTests
{
    [TestMethod]
    public void Validate_IdCannotBeNullRuleSet_IdIsNull_SuouldBeInvalidWithCorrectMessage()
    {
        var failMessage = "Entity Id cannot be null";
        var entity = new TestEntity { Id = null, Name = null };
        var validator = new Validator<TestEntity>(x => x.Id != null, failMessage);

        var result = validator.Validate(entity);

        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(failMessage, result.Message);
    }

    [TestMethod]
    public void Validate_IdCannotBeNullRuleSet_IdIsNotNull_SuouldBeValidAndNullMessage()
    {
        var failMessage = "Entity Id cannot be null";
        var entity = new TestEntity { Id = 42, Name = null };
        var validator = new Validator<TestEntity>(x => x.Id != null, failMessage);

        var result = validator.Validate(entity);

        Assert.IsTrue(result.IsValid);
        Assert.IsNull(result.Message);
    }
}
