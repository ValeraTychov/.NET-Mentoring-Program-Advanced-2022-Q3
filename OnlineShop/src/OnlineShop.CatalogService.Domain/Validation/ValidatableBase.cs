namespace OnlineShop.CatalogService.Domain.Validation;

public class ValidatableBase<T> : IValidatable where T : ValidatableBase<T>
{
    protected ValidatorBase<T> validator;

    public ValidationResult Validate()
    {
        return validator.Validate((T)this);
    }

    public void SetValidator(ValidatorBase<T> validator)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
}
