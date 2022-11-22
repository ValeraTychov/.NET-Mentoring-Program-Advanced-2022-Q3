using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Domain.Models;

namespace OnlineShop.CatalogService.Domain;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IBusPublisher _busPublisher;

    public ItemService(IItemRepository itemRepository, IBusPublisher busPublisher)
    {
        _itemRepository = itemRepository;
        _busPublisher = busPublisher;
    }

    public Range<Item> GetRange(int from = 0, int to = int.MaxValue)
    {
        return new Range<Item>
        {
            Entities = _itemRepository.GetRange(from, to),
            TotalCount = _itemRepository.GetCount(),
        };
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
        var result = AddOrUpdate(item, (e, r) => r.Update(e));
        
        if (!result.IsSuccess)
        {
            return result;
        }

        _busPublisher.PublishItemChanged(item, DateTime.UtcNow);

        return result;
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