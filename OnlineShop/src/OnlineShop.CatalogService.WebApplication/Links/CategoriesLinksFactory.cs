using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Extensions;
using OnlineShop.CatalogService.WebApplication.Entities;

namespace OnlineShop.CatalogService.WebApplication.Links;

public class CategoriesLinksFactory
{
    public static List<Link> Create(HttpRequest httpRequest, Page<Category> page)
    {
        var pageSubPath = Regex.Match(httpRequest.GetDisplayUrl(), ".+Page/");
        var apiSubPath = Regex.Match(httpRequest.GetDisplayUrl(), ".+api");

        var links = new List<Link>(10)
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

        links.Add(
            new Link
            {
                Href = $"{apiSubPath}/Category/{{id}}",
                Rel = "category",
                Method = "GET",
            });

        links.Add(new Link
            {
                Href = $"{apiSubPath}/Item/{{id}}",
                Rel = "item",
                Method = "GET",
            }
        );

        return links;
    }
}