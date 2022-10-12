using AutoMapper;
using OnlineShop.BLL.CartService.Entities;
using OnlineShop.DAL.CartService;

namespace OnlineShop.BLL.CartService;

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
        throw new NotImplementedException();
    }

    public void AddItem(Guid cartId, Item item)
    {
         
    }

    public void RemoveItem(Guid cartId, int itemId)
    {
        throw new NotImplementedException();
    }
}
