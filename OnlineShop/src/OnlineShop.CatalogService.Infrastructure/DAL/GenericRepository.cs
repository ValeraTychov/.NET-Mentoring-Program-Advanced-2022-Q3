using System.Xml;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class GenericRepository<TEntity, TDalEntity> : IRepository<TEntity>, IDisposable where TDalEntity : class
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<TDalEntity> Entities;
    protected readonly IMapper Mapper;

    public GenericRepository(IMapper mapper, DbContext dbContext)
    {
        Mapper = mapper;
        DbContext = dbContext;
        Entities = dbContext.Set<TDalEntity>();
    }

    public Type Type => typeof(TEntity);

    public IEnumerable<TEntity> Get()
    {
        return Entities.Select(dalEntity => Mapper.Map<TEntity>(dalEntity)).ToArray();
    }

    public TEntity Get(int id)
    {
        return Mapper.Map<TEntity>(Entities.Find(id));
    }

    public virtual void Add(TEntity entity)
    {
        var dalEntity = Mapper.Map<TDalEntity>(entity);
        Add(dalEntity);
    }

    protected void Add(TDalEntity dalEntity)
    {
        Entities.Add(dalEntity);
        DbContext.SaveChanges();
    }

    public virtual void Update(TEntity item)
    {
        var dalEntity = Mapper.Map<TDalEntity>(item);
        Update(dalEntity);
    }

    protected void Update(TDalEntity dalEntity)
    {
        Entities.Update(dalEntity);
        DbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        Entities.Remove(Entities.Find(id));
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}
