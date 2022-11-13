using OnlineShop.CatalogService.WebApplication.Entities;

namespace OnlineShop.CatalogService.WebApplication.Models;

public class GetItemsResponse
{
    public Page<Item> Page { get; set; }

    public List<Link> Links { get; set; }
}