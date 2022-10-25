namespace OnlineShop.CatalogService.Domain.Validation;

public abstract class ValidatorBase<T>
{
    public abstract ValidationResult Validate(T obj);

    public static ValidatorBase<T> operator |(ValidatorBase<T> validatorA, ValidatorBase<T> validatorB)
    {
        return new AggregateValidator<T>((x, y) => x | y, validatorA, validatorB);
    }

    public static ValidatorBase<T> operator &(ValidatorBase<T> validatorA, ValidatorBase<T> validatorB)
    {
        return new AggregateValidator<T>((x, y) => x & y, validatorA, validatorB);
    }
}
