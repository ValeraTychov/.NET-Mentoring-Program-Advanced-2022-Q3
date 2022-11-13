using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain;

public interface IItemRepository : IRepository<Item>
{
    public IEnumerable<Item> GetRange(int from = 0, int to = int.MaxValue);

    public IEnumerable<Item> GetByCategory(int categoryId);
}