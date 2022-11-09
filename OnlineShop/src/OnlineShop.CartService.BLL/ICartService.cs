using OnlineShop.CartService.BLL.Entities;

namespace OnlineShop.CartService.BLL;

public interface ICartService
{
    Cart? GetCart(Guid cartId);

    void AddItem(Guid cartId, Item item);

    void RemoveItem(Guid cartId, int itemId);
}
