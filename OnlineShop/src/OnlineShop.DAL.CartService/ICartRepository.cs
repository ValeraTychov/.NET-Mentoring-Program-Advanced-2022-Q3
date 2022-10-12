using OnlineShop.DAL.CartService.Entities;

namespace OnlineShop.DAL.CartService;

public interface ICartRepository
{
    public Cart Get(Guid cartId);

    public void AddOrUpdate(Cart cart);

    public void Remove(Guid cartId);
}
