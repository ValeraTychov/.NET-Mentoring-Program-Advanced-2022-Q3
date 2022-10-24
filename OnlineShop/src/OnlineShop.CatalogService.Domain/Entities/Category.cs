using OnlineShop.CatalogService.Domain.Validation;

namespace OnlineShop.CatalogService.Domain.Entities;

public class Category : ValidatableBase<Category>
{
    public Category()
    {
        SetDefaultValidator();
    }

    public int Id { get; set; }

    //Name – required, plain text, max length = 50.
    public string? Name { get; set; }

    //Image – optional, URL.
    public Uri? Image { get; set; }

    //Parent Category – optional
    public Category? Parent { get; set; }

    private void SetDefaultValidator()
    {
        validator = Validator.For<Category>(c => c.Id >= 0, "Id should be positive integer")
            & Validator.For<Category>(c => c.Name != null, "Name property is required")
            & Validator.For<Category>(c => c.Name?.Length <= 50, "Name field max length = 50");
    }
}
