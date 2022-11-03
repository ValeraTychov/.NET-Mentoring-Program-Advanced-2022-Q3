using OnlineShop.CatalogService.WebApplication.Entities;

namespace OnlineShop.CatalogService.WebApplication.Models;

public class GetCategoriesResponse
{
    public IEnumerable<Category> Categories { get; set; }
}
