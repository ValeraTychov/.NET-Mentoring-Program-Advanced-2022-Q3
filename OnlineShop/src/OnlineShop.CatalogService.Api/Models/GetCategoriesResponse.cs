using OnlineShop.CatalogService.Api.Entities;

namespace OnlineShop.CatalogService.Api.Models;

public class GetCategoriesResponse
{
    public Page<Category>? Page { get; set; }

    public List<Link>? Links { get; set; }
}
