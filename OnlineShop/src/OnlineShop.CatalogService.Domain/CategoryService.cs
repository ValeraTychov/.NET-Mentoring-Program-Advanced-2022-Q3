using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public class CategoryService
{
    private readonly IRepository<Category> _categoryRepository;

    public CategoryService(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _categoryRepository.Get();
    }

    public Category GetCategory(int id)
    {
        return _categoryRepository.Get(id);
    }

    public void Add(Category entity)
    {
        _categoryRepository.Add(entity);
    }

    public void Update(Category entity)
    {
        _categoryRepository.Update(entity);
    }

    public void Delete(int id)
    {
        _categoryRepository.Delete(id);
    }
}
