namespace OnlineShop.CatalogService.Domain.Models;

public class Range<T>
{
    public IEnumerable<T> Entities { get; set; }

    public int TotalCount { get; set; }
}
