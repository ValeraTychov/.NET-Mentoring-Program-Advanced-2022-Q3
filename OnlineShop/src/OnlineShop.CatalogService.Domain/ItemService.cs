using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public IEnumerable<Item> GetRange(int from = 0, int to = int.MaxValue)
    {
        return _itemRepository.GetRange(from, to);
    }

    public Item Get(int id)
    {
        return _itemRepository.Get(id);
    }

    public IEnumerable<Item> GetByCategory(int categoryId)
    {
        return _itemRepository.GetByCategory(categoryId);
    }

    public IOperationResult Add(Item item)
    {
        return AddOrUpdate(item, (e, r) => r.Add(e));
    }

    public IOperationResult Update(Item item)
    {
        return AddOrUpdate(item, (e, r) => r.Update(e));
    }

    private IOperationResult AddOrUpdate(Item item, Action<Item, IItemRepository> action)
    {
        var validationResult = item.Validate();
        if (!validationResult.IsValid)
        {
            return OperationResult.Fail(validationResult.Message!);
        }

        action(item, _itemRepository);
        return OperationResult.Success();
    }

    public void Delete(int id)
    {
        _itemRepository.Delete(id);
    }
}