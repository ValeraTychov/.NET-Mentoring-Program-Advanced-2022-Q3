namespace OnlineShop.CartService.Api.Entities;

public class Cart
{
    public Guid Id { get; set; }

    public List<Item> Items { get; set; } = new List<Item>();
}
