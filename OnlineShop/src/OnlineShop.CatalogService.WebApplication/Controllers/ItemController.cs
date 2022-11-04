using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.WebApplication.Entities;
using System.Data;
using DomainItem = OnlineShop.CatalogService.Domain.Entities.Item;

namespace OnlineShop.CatalogService.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly Domain.ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public ItemController(Domain.ICatalogService catalogService, IMapper mapper)
        {
            _catalogService = catalogService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            var items = GetAll();
            return items;
        }

        [HttpGet("{id}")]
        public Item? Get(int id)
        {
            var item = GetAll().FirstOrDefault(c => c.Id == id);
            return item;
        }

        private IEnumerable<Item> GetAll()
        {
            return _catalogService.Get<DomainItem>().Select(dc => _mapper.Map<Item>(dc));
        }

        [HttpPost]
        public void Post([FromBody] Item value)
        {
            var item = _mapper.Map<DomainItem>(value);
            _catalogService.Add(item);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            _catalogService.Update(_mapper.Map<DomainItem>(value));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _catalogService.Delete<Item>(id);
        }
    }
}
