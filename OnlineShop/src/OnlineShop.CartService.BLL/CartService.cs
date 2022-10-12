using AutoMapper;
using OnlineShop.CartService.BLL.Entities;
using OnlineShop.CartService.DAL;

using DalCart = OnlineShop.CartService.DAL.Entities.Cart;

namespace OnlineShop.CartService.BLL;

public class CartService : ICartService
{
    private readonly IMapper _mapper;
    private readonly ICartRepository _cartRepository;

    public CartService(IMapper mapper, ICartRepository cartRepository)
    {
        _mapper = mapper;
        _cartRepository = cartRepository;
    }

    public IEnumerable<Item> GetItems(Guid cartId)
    {
        var cart = GetCart(cartId);
        return cart?.Items ?? new List<Item>();
    }

    public void AddItem(Guid cartId, Item item)
    {
        var cart = GetCart(cartId);
        
        cart ??= new Cart { Id = cartId };

        cart.AddItem(item);
        AddOrUpdate(cart);
    }

    public void RemoveItem(Guid cartId, int itemId)
    {
        var cart = GetCart(cartId);

        if (cart == null)
        {
            return;
        }

        cart.RemoveItem(itemId);
        AddOrUpdate(cart);
    }

    private Cart? GetCart(Guid cartId)
    {
        return _mapper.Map<Cart>(_cartRepository.Get(cartId));
    }

    private void AddOrUpdate(Cart cart)
    {
        _cartRepository.AddOrUpdate(_mapper.Map<DalCart>(cart));
    }
}
