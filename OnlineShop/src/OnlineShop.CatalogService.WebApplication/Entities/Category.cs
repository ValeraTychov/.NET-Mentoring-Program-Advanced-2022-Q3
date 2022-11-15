namespace OnlineShop.CatalogService.WebApplication.Entities;

public class Category
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public Uri? Image { get; set; }

    public int? ParentId { get; set; }
}
