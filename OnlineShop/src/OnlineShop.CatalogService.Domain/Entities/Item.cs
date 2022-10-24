using OnlineShop.CatalogService.Domain.Validation;

namespace OnlineShop.CatalogService.Domain.Entities;

public class Item : ValidatableBase<Item>
{
    public Item()
    {
        SetDefaultValidator();
    }

    public int Id { get; set; }

    //Name – required, plain text, max length = 50.
    public string? Name { get; set; }

    //Description – optional, can contain html.
    public string? Description { get; set; }

    //Image – optional, URL.
    public Uri? Image { get; set; }

    //Category – required, one item can belong to only one category.
    public Category? Category { get; set; }
    
    //Price – required, money.
    public decimal Price { get; set; }

    //Amount – required, positive int.
    public int Amount { get; set; }

    private void SetDefaultValidator()
    {
        validator = Validator.For<Item>(item => item.Name != null, GenerateFieldIsRequiredString("Name"))
            & Validator.For<Item>(item => item.Name.Length <= 50, "Name field max length = 50")
            & Validator.For<Item>(item => item.Category != null, GenerateFieldIsRequiredString("Category"))
            & Validator.For<Item>(item => item.Amount > 0, "Amount field should be positive int");
    }

    private static string GenerateFieldIsRequiredString(string fieldName)
    {
        return $"{fieldName} field is required";
    }
}
