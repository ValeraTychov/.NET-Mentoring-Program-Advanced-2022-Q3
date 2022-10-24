namespace OnlineShop.CatalogService.Domain.Validation;

public class ValidationResult
{
    public bool IsValid { get; init; }

    public string? Message => Messages?.FirstOrDefault();

    public IEnumerable<string>? Messages { get; init; }
}
