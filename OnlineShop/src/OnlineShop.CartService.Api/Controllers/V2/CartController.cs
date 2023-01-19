using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CartService.Api.Entities;
using OnlineShop.CartService.BLL;

namespace OnlineShop.CartService.Api.Controllers.V2;

[ApiController]
[Route("api/v2/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartController(ICartService cartService, IMapper mapper)
    {
        _cartService = cartService;
        _mapper = mapper;
    }

    [HttpGet("{cartId}")]
    public IEnumerable<Item> Get(Guid cartId)
    {
        var cart = _mapper.Map<Cart>(_cartService.GetCart(cartId));
        return cart.Items;
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
