using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public class CategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _repository.Get();
    }

    public Category GetCategory(int id)
    {
        return _repository.Get(id);
    }

    public IOperationResult Add(Category category)
    {
        return AddOrUpdate(category, c => _repository.Add(c));
    }

    public IOperationResult Update(Category entity)
    {
        return AddOrUpdate(entity, c => _repository.Update(c));
    }

    private IOperationResult AddOrUpdate(Category category, Action<Category> action)
    {
        var validationResult = category.Validate();
        if (!validationResult.IsValid)
        {
            return OperationResult.Fail(validationResult.Message!);
        }

        action(category);
        return OperationResult.Success();
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}
