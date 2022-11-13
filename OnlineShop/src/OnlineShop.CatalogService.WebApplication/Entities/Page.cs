using OnlineShop.CatalogService.WebApplication.Pagination;

namespace OnlineShop.CatalogService.WebApplication.Entities;

public class Page<T>
{
    public Page()
    {
    }

    public Page(List<T> content, int totalItems, int pageNumber, int pageSize)
    {
        Content = content;
        Number = pageNumber;
        Total = PageCalculator.CalcTotalPages(totalItems, pageSize);
    }

    public List<T> Content { get; set; }

    public int Number { get; set; }

    public int Total { get; set; }

    public int First => 1;

    public int? Previous => Number > First ? Number - 1 : null;

    public int? Next => Number < Last ? Number + 1 : null;

    public int Last => Total - 1;
}
