﻿using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public interface IItemService
{
    Range<Item> GetRange(int from = 0, int to = int.MaxValue);

    Item Get(int id);

    IEnumerable<Item> GetByCategory(int categoryId);

    IOperationResult Add(Item item);

    IOperationResult Update(Item item);

    void Delete(int id);
}
