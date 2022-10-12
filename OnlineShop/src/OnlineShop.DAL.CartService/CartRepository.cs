using LiteDB;
using OnlineShop.DAL.CartService.Entities;

namespace OnlineShop.DAL.CartService;

public class CartRepository : ICartRepository
{
    public Cart Get(Guid cartId)
    {
        throw new NotImplementedException();
    }

    public void AddOrUpdate(Cart cart)
    {
        using (var db = new LiteDatabase(@"D:\MyData.db"))
        {
           
           var collection = db.GetCollection<Cart>();
           collection.Insert(cart);

            
        }
    }

    public void Remove(Guid cartId)
    {
        throw new NotImplementedException();
    }
}
