namespace OnlineShop.CatalogService.Domain.Validation;

public class AggregateValidator<T> : ValidatorBase<T>
{
    private readonly Func<bool, bool, bool> _validationRule;
    private readonly ValidatorBase<T>[] _validators;

    public AggregateValidator(Func<bool, bool, bool> validationRule, params ValidatorBase<T>[] validators)
    {
        _validationRule = validationRule;
        _validators = validators;
    }

    public override ValidationResult Validate(T obj)
    {
        var results = _validators.Select(validator => validator.Validate(obj)).ToArray();
        var isValid = results.Skip(1).Aggregate(results.First().IsValid, (acc, x) => _validationRule(acc, x.IsValid));
        return new ValidationResult
        {
            IsValid = isValid,
            Messages = isValid ? Array.Empty<string>() : results.Where(x => !x.IsValid).Select(x => x.Message).ToArray()
        };
    }
}
