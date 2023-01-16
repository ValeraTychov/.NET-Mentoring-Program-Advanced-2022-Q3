namespace OnlineShop.Messaging.Abstraction.Entities;

public class ItemChangedMessage
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public Uri? Image { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }

}