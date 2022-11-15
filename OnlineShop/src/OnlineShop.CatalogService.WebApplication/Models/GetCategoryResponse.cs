using OnlineShop.CatalogService.WebApplication.Entities;

namespace OnlineShop.CatalogService.WebApplication.Models
{
    public class GetCategoryResponse
    {
        public Category Category { get; set; }

        public List<Link> Links { get; set; }
    }
}
