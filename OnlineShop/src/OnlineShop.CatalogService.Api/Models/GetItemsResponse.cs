using OnlineShop.CatalogService.Api.Entities;

namespace OnlineShop.CatalogService.Api.Models;

public class GetItemsResponse
{
    public Page<Item> Page { get; set; }

    public List<Link> Links { get; set; }
}
