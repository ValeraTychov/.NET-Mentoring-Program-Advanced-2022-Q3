namespace OnlineShop.CatalogService.Domain.Validation;

public interface IValidatable
{
    ValidationResult Validate();
}
