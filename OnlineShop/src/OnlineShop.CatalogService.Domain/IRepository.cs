﻿namespace OnlineShop.CatalogService.Domain;

public interface IRepository<TEntity>
{
    IEnumerable<TEntity> Get();

    TEntity Get(int id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(int id);
}