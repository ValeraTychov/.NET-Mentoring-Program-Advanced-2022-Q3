namespace OnlineShop.CatalogService.Domain.Validation;

internal interface IValidator<TEntity>
{
    (bool isValid, string? message) Validate(TEntity entity);
}
