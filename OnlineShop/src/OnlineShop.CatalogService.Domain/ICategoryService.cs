using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public interface ICategoryService
{
    Range<Category> GetRange(int from = 0, int to = int.MaxValue);

    Category Get(int id);

    IEnumerable<Category> GetChildren(int parentId);

    IOperationResult Add(Category entity);

    IOperationResult Update(Category entity);

    void Delete(int id);
}
