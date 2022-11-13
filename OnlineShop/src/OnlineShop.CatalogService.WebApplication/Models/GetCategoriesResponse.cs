using OnlineShop.CatalogService.WebApplication.Entities;

namespace OnlineShop.CatalogService.WebApplication.Models;

public class GetCategoriesResponse
{
    public Page<Category> Page { get; set; }

    public List<Link> Links { get; set; }
}
