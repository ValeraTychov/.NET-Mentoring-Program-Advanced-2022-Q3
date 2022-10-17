namespace OnlineShop.CatalogService.Domain.Entities;

public class Item
{
    public int Id { get; set; }

    //Name – required, plain text, max length = 50.
    public string Name { get; set; }

    //Description – optional, can contain html.
    public string? Description { get; set; }

    //Image – optional, URL.
    public Uri? Image { get; set; }

    //Category – required, one item can belong to only one category.
    public Category Category { get; set; }
    
    //Price – required, money.
    public decimal Price { get; set; }

    //Amount – required, positive int.
    public int Amount { get; set; }
}
