﻿using AutoMapper;
using OnlineShop.CartService.BLL.Entities;
using OnlineShop.CartService.DAL;
using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;
using DalCart = OnlineShop.CartService.DAL.Entities.Cart;

namespace OnlineShop.CartService.BLL;

public class CartService : ICartService, IDisposable
{
    private readonly IMapper _mapper;
    private readonly ICartRepository _cartRepository;
    private readonly ISubscriber<ItemChangedMessage> _subscriber;

    public CartService(IMapper mapper, ICartRepository cartRepository, ISubscriber<ItemChangedMessage> subscriber)
    {
        _mapper = mapper;
        _cartRepository = cartRepository;
        _subscriber = subscriber;
        _subscriber.Subscribe(OnItemChanged);
    }

    public IEnumerable<Cart> GetCarts()
    {
        return _cartRepository.Get().Select(dc => _mapper.Map<Cart>(dc));
    }

    public Cart? GetCart(Guid cartId)
    {
        return _mapper.Map<Cart>(_cartRepository.Get(cartId));
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

    private void AddOrUpdate(Cart cart)
    {
        _cartRepository.AddOrUpdate(_mapper.Map<DalCart>(cart));
    }

    private void OnItemChanged(ItemChangedMessage eventParameters)
    {
        foreach (var dalCart in _cartRepository.Get())
        {
            var cart = _mapper.Map<Cart>(dalCart);
            var item = cart.Items.Find(i => i.Id == eventParameters.Id);
            if (item == default)
            {
                continue;
            }

            UpdateItem(item, eventParameters);
            AddOrUpdate(cart);
        }
    }

    private void UpdateItem(Item item, ItemChangedMessage eventParameters)
    {
        item.Image.Url = eventParameters.Image?.AbsoluteUri;
        item.Name = eventParameters.Name;
        item.Price = eventParameters.Price;
    }

    public void Dispose()
    {
        _subscriber.Dispose();
    }
}
