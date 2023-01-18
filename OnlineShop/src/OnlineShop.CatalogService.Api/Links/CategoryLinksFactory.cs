using Microsoft.AspNetCore.Http.Extensions;
using OnlineShop.CatalogService.Api.Entities;

namespace OnlineShop.CatalogService.Api.Links;

public static class CategoryLinksFactory
{
    public static List<Link> Create(HttpRequest httpRequest)
    {
        return new List<Link>
        {
            new Link
            {
                Href = $"{httpRequest.GetDisplayUrl()}",
                Rel = "self",
                Method = "GET",
            },
            new Link
            {
                Href = $"{httpRequest.GetDisplayUrl()}/Children",
                Rel = "children",
                Method = "GET",
            },
            new Link
            {
                Href = $"{httpRequest.GetDisplayUrl()}/Items",
                Rel = "items",
                Method = "GET",
            }
        };
    }
}
