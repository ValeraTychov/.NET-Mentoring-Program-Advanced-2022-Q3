using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.WebApplication.Entities;
using OnlineShop.CatalogService.Domain;
using DomainItem = OnlineShop.CatalogService.Domain.Entities.Item;

namespace OnlineShop.CatalogService.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return _mapper.Map<Item>(_itemService.Get(id));
        }
        
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            var item = _mapper.Map<DomainItem>(value);
            _itemService.Add(item);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            _itemService.Update(_mapper.Map<DomainItem>(value));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _itemService.Delete(id);
        }
    }
}
