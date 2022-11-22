using OnlineShop.CartService.DAL.Entities;

namespace OnlineShop.CartService.DAL;

public interface ICartRepository
{
    IEnumerable<Cart> Get();

    public Cart Get(Guid cartId);

    public void AddOrUpdate(Cart cart);
}
