using OnlineShop.CatalogService.Api.Entities;

namespace OnlineShop.CatalogService.Api.Models
{
    public class GetCategoryResponse
    {
        public Category Category { get; set; }

        public List<Link> Links { get; set; }
    }
}
