namespace OnlineShop.CatalogService.Api.Pagination
{
    public class PageCalculator
    {
        public static (int from, int to) CalcIndexRange(int pageNumber, int pageSize)
        {
            var from = (pageNumber - 1) * pageSize;
            var to = pageNumber * pageSize - 1;
            return (from, to);
        }

        public static int CalcTotalPages(int totalItems, int pageSize)
        {
            return (totalItems - 1) / pageSize + 1;
        }
    }
}
