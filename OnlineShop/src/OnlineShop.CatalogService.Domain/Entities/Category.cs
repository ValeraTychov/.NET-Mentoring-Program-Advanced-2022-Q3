namespace OnlineShop.CatalogService.Domain.Entities;

public class Category
{
    private string _name;

    //Name – required, plain text, max length = 50.
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    //Image – optional, URL.
    public Uri? Image { get; set; }

    //Parent Category – optional
    public Category? Parent { get; set; }
}
