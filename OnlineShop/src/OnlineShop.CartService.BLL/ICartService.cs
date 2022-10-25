using OnlineShop.CartService.BLL.Entities;

namespace OnlineShop.CartService.BLL;

public interface ICartService
{
    IEnumerable<Item> GetItems(Guid cartId);

    void AddItem(Guid cartId, Item item);

    void RemoveItem(Guid cartId, int itemId);
}
