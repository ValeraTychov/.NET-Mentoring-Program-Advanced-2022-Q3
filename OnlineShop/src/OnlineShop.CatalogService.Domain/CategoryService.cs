using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> GetRange(int from = 0, int to = int.MaxValue)
    {
        return _categoryRepository.GetRange(from, to);
    }

    public Category Get(int id)
    {
        return _categoryRepository.Get(id);
    }

    public IEnumerable<Category> GetChildren(int parentId)
    {
        return _categoryRepository.GetChildren(parentId);
    }

    public IOperationResult Add(Category entity)
    {
        return AddOrUpdate(entity, (e, r) => r.Add(e));
    }

    public IOperationResult Update(Category entity)
    {
        return AddOrUpdate(entity, (e, r) => r.Update(e));
    }

    private IOperationResult AddOrUpdate(Category entity, Action<Category, ICategoryRepository> action)
    {
        var validationResult = entity.Validate();
        if (!validationResult.IsValid)
        {
            return OperationResult.Fail(validationResult.Message!);
        }

        action(entity, _categoryRepository);
        return OperationResult.Success();
    }

    public void Delete(int id)
    {
        _categoryRepository.Delete(id);
    }
}