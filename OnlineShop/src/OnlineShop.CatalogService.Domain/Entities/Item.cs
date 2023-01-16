using OnlineShop.CatalogService.Domain.Validation;

namespace OnlineShop.CatalogService.Domain.Entities;

public class Item : ValidatableBase<Item>
{
    public Item()
    {
        SetDefaultValidator();
    }

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Uri? Image { get; set; }

    public Category Category { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }

    private void SetDefaultValidator()
    {
        EntityValidator = Validator.For<Item>(item => item.Name != null, GenerateFieldIsRequiredString("Name"))
            & Validator.For<Item>(item => item.Name.Length <= 50, "Name property max length = 50")
            & Validator.For<Item>(item => item.Category != null, GenerateFieldIsRequiredString("Category"))
            & Validator.For<Item>(item => item.Amount > 0, "Amount property should be positive int")
            & Validator.For<Item>(item => item.Price > 0M, "Price property should be positive decimal");
    }

    private static string GenerateFieldIsRequiredString(string fieldName)
    {
        return $"{fieldName} property is required";
    }
}
