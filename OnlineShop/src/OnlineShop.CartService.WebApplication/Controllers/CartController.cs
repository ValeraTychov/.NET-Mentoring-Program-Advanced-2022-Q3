﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CartService.BLL;
using OnlineShop.CartService.WebApplication.Entities;

namespace OnlineShop.CartService.WebApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartController(ICartService cartService, IMapper mapper)
    {
        _cartService = cartService;
        _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<Cart> Get()
    {
        throw new NotImplementedException();
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