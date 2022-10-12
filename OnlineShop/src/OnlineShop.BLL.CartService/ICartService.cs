using OnlineShop.BLL.CartService.Entities;

namespace OnlineShop.BLL.CartService;

public interface ICartService
{
    IEnumerable<Item> GetItems(Guid cartId);

    void AddItem(Guid cartId, Item item);

    void RemoveItem(Guid cartId, int itemId);
}
