using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;
using OnlineShop.CatalogService.Domain.Validation;

namespace OnlineShop.CatalogService.Domain;

public class CategoryService
{
    private readonly IRepository<Category> _repository;
    private readonly IValidator<Category> _validator;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
        _validator = CategoryValidator.Create();
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
        var validationResult = _validator.Validate(category);
        if (!validationResult.isValid)
        {
            return OperationResult.Fail(validationResult.message!);
        }

        action(category);
        return OperationResult.Success();
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}
