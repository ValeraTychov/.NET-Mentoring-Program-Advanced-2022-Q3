namespace OnlineShop.CatalogService.Domain.Validation;

public class ValidatableBase<TEntity> : IValidatable where TEntity : ValidatableBase<TEntity>
{
    protected ValidatorBase<TEntity> EntityValidator;

    public ValidationResult Validate()
    {
        return EntityValidator.Validate((TEntity)this);
    }
}
