﻿using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Extensions;
using OnlineShop.CatalogService.Api.Entities;

namespace OnlineShop.CatalogService.Api.Links;

public static class ItemsLinksFactory
{
    public static List<Link> Create(HttpRequest httpRequest, Page<Item> page)
    {
        var apiSubPath = Regex.Match(httpRequest.GetDisplayUrl(), ".+api");

        var links = PageLinksFactory.Create(httpRequest, page);

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
