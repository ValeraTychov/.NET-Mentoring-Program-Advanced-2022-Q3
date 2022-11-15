namespace OnlineShop.CartService.BLL.Entities;

public class Image
{
    private Uri _url;

    public string Url
    {
        get { return _url.ToString(); }
        set { _url = Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out Uri? result) ? result : null; }
    }

    public string AltText { get; set; }
}
