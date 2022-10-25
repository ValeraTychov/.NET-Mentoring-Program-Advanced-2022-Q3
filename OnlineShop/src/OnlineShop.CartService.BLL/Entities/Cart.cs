namespace OnlineShop.CartService.BLL.Entities;

public class Cart
{
    public Guid Id { get; set; }

    public List<Item> Items { get; set; } = new List<Item>();

    public bool HasItem(int itemId, out Item? item)
    {
        item = Items.FirstOrDefault(i => i.Id == itemId);
        return item != null;
    }

    public void AddItem(Item item)
    {
        if(this.HasItem(item.Id, out var itemInCart))
        {
            itemInCart!.Quantity += item.Quantity;
            return;
        }

        Items.Add(item);
    }

    public void RemoveItem(int itemId)
    {
        if (!this.HasItem(itemId, out Item itemInCart))
        {
            return;
        }

        itemInCart!.Quantity--;

        if (itemInCart.Quantity <= 0)
        {
            Items.Remove(itemInCart);
        }
    }
}
