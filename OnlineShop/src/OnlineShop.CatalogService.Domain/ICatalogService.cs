using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public interface ICatalogService
{
    IEnumerable<TEntity> Get<TEntity>();

    TEntity Get<TEntity>(int id);

    IOperationResult Add<TEntity>(TEntity entity);

    IOperationResult Update<TEntity>(TEntity entity);

    void Delete<TEntity>(int id);
}
