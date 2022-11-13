namespace OnlineShop.CatalogService.WebApplication.Models
{
    public class Page<T>
    {
        private IEnumerable<T> _source;
        private int _pageSize;

        private Page(){}

        public Page(IEnumerable<T> source, int pageSize)
        {
            _source = source;
            _pageSize = pageSize;
        }

        public List<T> PageContent { get; private set; }

        public int Number { get; private set; }

        public int Total { get; private set; }

        public Page<T> GetPage(int number)
        {
            return new Page<T>
            {
                PageContent = _source.Skip(number * _pageSize).Take(_pageSize).ToList(),
                Number = number,
                Total = _source.Count() % _pageSize,
            };
        }
    }
}
