namespace OnlineShop.CatalogService.Infrastructure.DAL.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Uri? Image { get; set; }

    public int? ParentId { get; set; }

    public Category? Parent { get; set; }
}
