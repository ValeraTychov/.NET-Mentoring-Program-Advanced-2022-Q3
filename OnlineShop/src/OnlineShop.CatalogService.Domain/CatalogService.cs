using OnlineShop.CatalogService.Domain.Models;
using OnlineShop.CatalogService.Domain.Validation;

namespace OnlineShop.CatalogService.Domain;

public class CatalogService : ICatalogService
{
    private readonly Dictionary<Type, IRepository> _repositories;

    public CatalogService(params IRepository[] repositories)
    {
        _repositories = repositories.ToDictionary(repository => repository.Type);
    }

    private IRepository<TEntity> GetRepository<TEntity>()
    {
        return (IRepository<TEntity>)_repositories[typeof(TEntity)];
    }

    public IEnumerable<TEntity> Get<TEntity>()
    {
        return GetRepository<TEntity>().Get();
    }

    public TEntity Get<TEntity>(int id)
    {
        return GetRepository<TEntity>().Get(id);
    }

    public IOperationResult Add<TEntity>(TEntity entity)
    {
        return AddOrUpdate(entity, (e, r) => r.Add(e));
    }

    public IOperationResult Update<TEntity>(TEntity entity)
    {
        return AddOrUpdate(entity, (e, r) => r.Update(e));
    }

    private IOperationResult AddOrUpdate<TEntity>(TEntity entity, Action<TEntity, IRepository<TEntity>> action)
    {
        var validationResult = ((IValidatable)entity).Validate();
        if (!validationResult.IsValid)
        {
            return OperationResult.Fail(validationResult.Message!);
        }

        action(entity, GetRepository<TEntity>());
        return OperationResult.Success();
    }

    public void Delete<TEntity>(int id)
    {
        GetRepository<TEntity>().Delete(id);
    }
}
