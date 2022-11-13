using OnlineShop.CatalogService.WebApplication.Entities;

namespace OnlineShop.CatalogService.WebApplication.Pagination
{
    public class PageManager<T>
    {
        private IEnumerable<T> _source;
        private int _pageSize;


        public PageManager(IEnumerable<T> source, int pageSize)
        {
            _source = source;
            _pageSize = pageSize > 0 ? _pageSize : throw new ArgumentOutOfRangeException(nameof(pageSize), "Should be positive :)");
        }
                

        public Page<T> CreatePage(int number)
        {
            return new Page<T>
            {
                Content = _source.Skip((number - 1) * _pageSize).Take(_pageSize).ToList(),
                Number = number,
                Total = (_source.Count() - 1) / _pageSize + 1,
            };
        }
    }
}
