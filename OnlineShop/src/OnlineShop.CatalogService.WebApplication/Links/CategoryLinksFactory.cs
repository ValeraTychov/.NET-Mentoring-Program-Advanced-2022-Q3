using Microsoft.AspNetCore.Http.Extensions;

namespace OnlineShop.CatalogService.WebApplication.Links;

public class CategoryLinksFactory
{
    public static Dictionary<string, string> Create(HttpRequest httpRequest)
    {
        return new Dictionary<string, string>
        {
            ["self"] = $"{httpRequest.GetDisplayUrl()}",
            ["children"] = $"{httpRequest.GetDisplayUrl()}/Children",
            ["items"] = $"{httpRequest.GetDisplayUrl()}/Items",
        };
    }
}
