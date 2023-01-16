using LiteDB;
using OnlineShop.CartService.DAL.Entities;

namespace OnlineShop.CartService.DAL;

public class CartRepository : ICartRepository
{
    private readonly string _connectionString = "MyData.db";

    public IEnumerable<Cart> Get()
    {
        IEnumerable<Cart> collection = null;
        ManipulateCollection<Cart>(c => collection = c.FindAll().ToArray());

        return collection;
    }

    public Cart Get(Guid cartId)
    {
        Cart? cart = null;
        ManipulateCollection<Cart>(c => cart = c.FindOne(x => x.Id == cartId));

        return cart;
    }

    public void AddOrUpdate(Cart cart)
    {
        ManipulateCollection<Cart>(c =>
        {
            if (!c.Update(cart))
            {
                c.Insert(cart);
            }
        });
    }

    private void ManipulateCollection<T>(Action<ILiteCollection<T>> action)
    {
        using (var db = new LiteDatabase(_connectionString))
        {
            var collection = db.GetCollection<T>("carts");
            action(collection);
        }
    }
}
