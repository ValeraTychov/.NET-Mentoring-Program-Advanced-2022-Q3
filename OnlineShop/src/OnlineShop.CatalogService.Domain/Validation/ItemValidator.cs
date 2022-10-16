using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain.Validation;

internal class ItemValidator
{
    private ItemValidator() { }

    public static IValidator<Item> Create()
    {
        return new GenericValidator<Item>(
            (item => item.Name != null, GenerateFieldIsRequiredString("Name")),
            (item => item.Name.Length <= 50, "Name field max length = 50"),
            (item => item.Category != null, GenerateFieldIsRequiredString("Category")),
            (item => item.Amount > 0, "Amount field should be positive int"));
    }

    private static string GenerateFieldIsRequiredString(string fieldName)
    {
        return $"{fieldName} field is required";
    }
}
