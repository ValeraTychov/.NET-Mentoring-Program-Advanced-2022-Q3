using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain;

public interface ICategoryRepository : IRepository<Category>
{
    public IEnumerable<Category> GetRange(int from = 0, int to = int.MaxValue);

    public IEnumerable<Category> GetChildren(int parentId);

    public int GetCount();
}