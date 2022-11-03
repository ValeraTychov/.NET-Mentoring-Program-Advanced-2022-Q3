namespace OnlineShop.CatalogService.Domain;

public interface IRepository<TEntity> : IRepository
{
    IEnumerable<TEntity> Get();

    TEntity Get(int id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(int id);
}

public interface IRepository
{
    Type Type { get; }
}