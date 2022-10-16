using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain.Validation;

internal class CategoryValidator
{
    private CategoryValidator() { }

    public static IValidator<Category> Create()
    {
        return new GenericValidator<Category>(
            (category => category.Name != null, "Name field is required;"),
            (category => category.Name.Length <= 50, "Name field max length = 50"));
    }
}
