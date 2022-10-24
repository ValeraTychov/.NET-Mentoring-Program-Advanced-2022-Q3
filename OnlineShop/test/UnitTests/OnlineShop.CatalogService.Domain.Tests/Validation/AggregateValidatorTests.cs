using OnlineShop.CatalogService.Domain.Validation;

namespace OnlineShop.CatalogService.Domain.Tests.Validation;

[TestClass]
public class AggregateValidatorTests
{
    [TestMethod]
    public void Validate_IdAndNameCannotBeNull_IdAndNameAreNull_SuouldBeInvalidWithCorrectMessage()
    {
        var failMessageForId = "Property Id cannot be null";
        var failMessageForName = "Property Name cannot be null";
        var expectedMessages = new string[] { failMessageForId, failMessageForName };
        var entity = new TestEntity { Id = null, Name = null };
        var validator = new Validator<TestEntity>(x => x.Id != null, failMessageForId)
            & new Validator<TestEntity>(x => x.Name != null, failMessageForName);

        var result = validator.Validate(entity);

        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(failMessageForId, result.Message);

        CollectionAssert.AreEqual(expectedMessages, result.Messages!.ToArray());
    }

    [TestMethod]
    public void Validate_IdAndNameCannotBeNull_IdIsNull_SuouldBeInvalidWithCorrectMessage()
    {
        var failMessageForId = "Property Id cannot be null";
        var failMessageForName = "Property Name cannot be null";
        var expectedMessages = new string[] { failMessageForId };
        var entity = new TestEntity { Id = null, Name = "Test" };
        var validator = new Validator<TestEntity>(x => x.Id != null, failMessageForId)
            & new Validator<TestEntity>(x => x.Name != null, failMessageForName);

        var result = validator.Validate(entity);

        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(failMessageForId, result.Message);

        CollectionAssert.AreEqual(expectedMessages, result.Messages!.ToArray());
    }

    [TestMethod]
    public void Validate_IdAndNameCannotBeNull_NameIsNull_SuouldBeInvalidWithCorrectMessage()
    {
        var failMessageForId = "Property Id cannot be null";
        var failMessageForName = "Property Name cannot be null";
        var expectedMessages = new string[] { failMessageForName };
        var entity = new TestEntity { Id = 42, Name = null };
        var validator = new Validator<TestEntity>(x => x.Id != null, failMessageForId)
            & new Validator<TestEntity>(x => x.Name != null, failMessageForName);

        var result = validator.Validate(entity);

        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(failMessageForName, result.Message);

        CollectionAssert.AreEqual(expectedMessages, result.Messages!.ToArray());
    }

    [TestMethod]
    public void Validate_IdAndNameCannotBeNull_TestEntityIsValid_SuouldBeValidWithNullMessage()
    {
        var failMessageForId = "Property Id cannot be null";
        var failMessageForName = "Property Name cannot be null";
        var expectedMessages = Array.Empty<string>();
        var entity = new TestEntity { Id = 42, Name = "Test" };
        var validator = new Validator<TestEntity>(x => x.Id != null, failMessageForId)
            & new Validator<TestEntity>(x => x.Name != null, failMessageForName);

        var result = validator.Validate(entity);

        Assert.IsTrue(result.IsValid);
        Assert.IsNull(result.Message);

        CollectionAssert.AreEqual(expectedMessages, result.Messages!.ToArray());
    }

    [TestMethod]
    public void Validate_NameOrSurnameShouldBeSpecified_NameAndSurnameAreNull_SuouldBeInvalidWithCorrectMessage()
    {
        var failMessageForName = "Name or surname should be specified";
        var failMessageForSurname = "Name or surname should be specified";
        var expectedMessages = new string[] { failMessageForName, failMessageForSurname };
        var entity = new TestEntity { Name = null, Surname = null };
        var validator = new Validator<TestEntity>(x => x.Name != null, failMessageForName)
            | new Validator<TestEntity>(x => x.Surname != null, failMessageForSurname);

        var result = validator.Validate(entity);

        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(failMessageForName, result.Message);

        CollectionAssert.AreEqual(expectedMessages, result.Messages!.ToArray());
    }

    [TestMethod]
    public void Validate_NameOrSurnameShouldBeSpecified_SurnameIsNull_SuouldBeValidWithNullMessage()
    {
        var failMessageForName = "Name or surname should be specified";
        var failMessageForSurname = "Name or surname should be specified";
        var expectedMessages = Array.Empty<string>();
        var entity = new TestEntity { Name = "John", Surname = null };
        var validator = new Validator<TestEntity>(x => x.Name != null, failMessageForName)
            | new Validator<TestEntity>(x => x.Surname != null, failMessageForSurname);

        var result = validator.Validate(entity);

        Assert.IsTrue(result.IsValid);
        Assert.IsNull(result.Message);

        CollectionAssert.AreEqual(expectedMessages, result.Messages!.ToArray());
    }
}
