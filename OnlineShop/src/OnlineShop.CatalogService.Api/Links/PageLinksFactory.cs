using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Extensions;
using OnlineShop.CatalogService.Api.Entities;

namespace OnlineShop.CatalogService.Api.Links;

public class PageLinksFactory
{
    public static List<Link> Create<T>(HttpRequest httpRequest, Page<T> page)
    {
        var pageSubPath = Regex.Match(httpRequest.GetDisplayUrl(), ".+Page/");
        var links = new List<Link>(8)
        {
            new Link
            {
                Href = $"{httpRequest.GetDisplayUrl()}",
                Rel = "self",
                Method = "GET",
            },
            new Link
            {
                Href = $"{pageSubPath}{page.First}",
                Rel = "first-page",
                Method = "GET",
            },

            new Link
            {
                Href = $"{pageSubPath}{page.Last}",
                Rel = "last-page",
                Method = "GET",
            }
        };

        if (page.Previous.HasValue)
        {
            links.Add(new Link
            {
                Href = $"{pageSubPath}{page.Previous}",
                Rel = "previous-page",
                Method = "GET",
            });
        }

        if (page.Next.HasValue)
        {
            links.Add(new Link
            {
                Href = $"{pageSubPath}{page.Next}",
                Rel = "next-page",
                Method = "GET",
            });
        }

        return links;
    }
}
