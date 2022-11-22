using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CartService.BLL;
using OnlineShop.CartService.WebApplication.Entities;

namespace OnlineShop.CartService.WebApplication.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartController(ICartService cartService, IMapper mapper)
    {
        _cartService = cartService;
        _mapper = mapper;
    }

    [HttpGet("Carts")]
    public IEnumerable<Cart> Get()
    {
        return _cartService.GetCarts().Select(bc => _mapper.Map<Cart>(bc));
    }

    [HttpGet("{cartId}")]
    public Cart Get(Guid cartId)
    {
        return _mapper.Map<Cart>(_cartService.GetCart(cartId));
    }

    [Route("{cartId}/Item")]
    [HttpPost]
    public void AddItem(Guid cartId, [FromBody] Item item)
    {
        _cartService.AddItem(cartId, _mapper.Map<BLL.Entities.Item>(item));
    }

    [HttpDelete("{cartId}/Item/{itemId}")]
    public void Delete(Guid cartId, int itemId)
    {
        _cartService.RemoveItem(cartId, itemId);
    }
}
