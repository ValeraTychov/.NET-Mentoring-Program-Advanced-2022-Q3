using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.WebApplication.Entities;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShop.CatalogService.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
