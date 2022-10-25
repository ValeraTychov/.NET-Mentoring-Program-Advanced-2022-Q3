using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class GenericRepository<TEntity, TDalEntity> : IRepository<TEntity>, IDisposable where TDalEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TDalEntity> _entities;
    private readonly IMapper _mapper;

    public GenericRepository(IMapper mapper, DbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _entities = dbContext.Set<TDalEntity>();
    }

    public IEnumerable<TEntity> Get()
    {
        return _entities.Select(dalEntity => _mapper.Map<TEntity>(dalEntity)).ToArray();
    }

    public TEntity Get(int id)
    {
        return _mapper.Map<TEntity>(_entities.Find(id));
    }

    public void Add(TEntity entity)
    {
        _entities.Add(_mapper.Map<TDalEntity>(entity));
        _dbContext.SaveChanges();
    }
    public void Update(TEntity entity)
    {
        _entities.Update(_mapper.Map<TDalEntity>(entity));
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {  
        _entities.Remove(_entities.Find(id));
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
