namespace OnlineShop.CatalogService.Api.Entities;

public class Item
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Uri? Image { get; set; }

    public int CategoryId { get; set; }
    
    public decimal Price { get; set; }

    public int Amount { get; set; }
}
