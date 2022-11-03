using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.WebApplication.Entities;
using OnlineShop.CatalogService.WebApplication.Models;
using System.Net;
using DomainCategory = OnlineShop.CatalogService.Domain.Entities.Category;

namespace OnlineShop.CatalogService.WebApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly Domain.ICatalogService _catalogService;
    private readonly IMapper _mapper;

    public CategoryController(Domain.ICatalogService catalogService, IMapper mapper)
    {
        _catalogService = catalogService;
        _mapper = mapper;
    }  

    [HttpGet]
    public GetCategoriesResponse Get()
    {
        var categories = GetAll();

        return new GetCategoriesResponse { Categories = categories };
    }

    private IEnumerable<Category> GetAll()
    {
        return _catalogService.Get<DomainCategory>().Select(dc => _mapper.Map<Category>(dc));
    }

    [HttpGet("{id}")]
    public ActionResult<Category> Get(int id)
    {
        var category = GetAll().FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    //[HttpGet("{parentId}")]
    //public ActionResult<Category> GetByParentId(int parentId)
    //{
    //    var category = GetAll().Where(c => c.Parent.Id == id);

    //    if (category == null)
    //    {
    //        return NotFound();
    //    }

    //    return category;
    //}

    [HttpPost]
    public void Post([FromBody] Category value)
    {
        _catalogService.Add(_mapper.Map<DomainCategory>(value));
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Category value)
    {
        
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
