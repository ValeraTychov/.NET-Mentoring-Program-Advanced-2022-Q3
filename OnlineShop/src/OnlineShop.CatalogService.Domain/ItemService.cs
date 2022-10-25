using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Domain;

public class ItemService
{
    private readonly IRepository<Item> _itemRepository;

    public ItemService(IRepository<Item> itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public IEnumerable<Item> GetItems()
    {
        return _itemRepository.Get();
    }

    public Item GetItem(int id)
    {
        return _itemRepository.Get(id);
    }

    public void Add(Item entity)
    {
        _itemRepository.Add(entity);
    }

    public void Update(Item entity)
    {
        _itemRepository.Update(entity);
    }

    public void Delete(int id)
    {
        _itemRepository.Delete(id);
    }
}
