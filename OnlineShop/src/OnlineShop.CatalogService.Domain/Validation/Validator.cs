namespace OnlineShop.CatalogService.Domain.Validation;

public class Validator<T> : ValidatorBase<T>
{
    public Validator(Func<T, bool> validationRule, string failMessage)
    {
        ValidationRule = validationRule ?? throw new ArgumentNullException(nameof(validationRule));
        FailMessage = failMessage ?? throw new ArgumentNullException(nameof(failMessage));
    }

    protected Func<T, bool> ValidationRule { get; set; }

    private string? FailMessage { get; set; }

    public override ValidationResult Validate(T obj)
    {
        var isValid = ValidationRule(obj);

        return new ValidationResult
        {
            IsValid = isValid,
            Messages = isValid ? Enumerable.Empty<string>() : new[] { FailMessage }
        };
    }
}

public class Validator
{
    public static ValidatorBase<T> For<T>(Func<T, bool> validationRule, string failMessage)
    {
        return new Validator<T>(validationRule, failMessage);
    }
}
