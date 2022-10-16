namespace OnlineShop.CatalogService.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    //Name – required, plain text, max length = 50.
    public string Name { get; set; }

    //Image – optional, URL.
    public Uri? Image { get; set; }

    //Parent Category – optional
    public Category? Parent { get; set; }
}
