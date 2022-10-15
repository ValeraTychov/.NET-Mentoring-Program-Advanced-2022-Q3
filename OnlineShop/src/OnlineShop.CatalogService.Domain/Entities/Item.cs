namespace OnlineShop.CatalogService.Domain.Entities;

public class Item
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
