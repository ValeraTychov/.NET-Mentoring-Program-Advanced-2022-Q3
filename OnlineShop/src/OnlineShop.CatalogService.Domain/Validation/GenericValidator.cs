namespace OnlineShop.CatalogService.Domain.Validation;

internal class GenericValidator<TEntity> : IValidator<TEntity>
{
    private readonly (Predicate<TEntity> predicate, string message)[] _validators;
    
    public GenericValidator(params (Predicate<TEntity> predicate, string message)[] validators)
    {
        _validators = validators;
    }

    public (bool isValid, string? message) Validate(TEntity entity)
    {
        foreach (var validator in _validators)
        {
            if (!validator.predicate(entity))
            {
                return (false, validator.message);
            }
        }

        return (true, null);
    }
}
