using OnlineShop.CartService.DAL.Entities;

namespace OnlineShop.CartService.DAL;

public interface ICartRepository
{
    public Cart? Get(Guid cartId);

    public void AddOrUpdate(Cart cart);
}
