using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.WebApplication.Entities;
using OnlineShop.CatalogService.WebApplication.Links;
using OnlineShop.CatalogService.WebApplication.Models;
using DomainCategory = OnlineShop.CatalogService.Domain.Entities.Category;
using DomainItem = OnlineShop.CatalogService.Domain.Entities.Item;

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

    [HttpGet("{id}")]
    public GetCategoryResponse? Get(int id)
    {
        var category = GetAll().FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return null;
        }

        return new GetCategoryResponse
        {
            Category = category,
            Links = CategoryLinksFactory.Create(Request)
        };
    }

    private IEnumerable<Category> GetAll()
    {
        return _catalogService.Get<DomainCategory>().Select(dc => _mapper.Map<Category>(dc));
    }

    [Route("{id}/Children")]
    [HttpGet]
    public IEnumerable<Category> GetChildrenCategories(int id)
    {
        var categories = GetAll().Where(c => c.ParentId == id);
        return categories;
    }

    [Route("{id}/Items")]
    [HttpGet]
    public IEnumerable<Item> GetItems(int id)
    {
        var items = _catalogService
            .Get<DomainItem>()
            .Where(i => i.Category.Id == id)
            .Select(i => _mapper.Map<Item>(i));
        return items;
    }

    [HttpPost]
    public void Post([FromBody] Category value)
    {
        _catalogService.Add(_mapper.Map<DomainCategory>(value));
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Category value)
    {
        _catalogService.Update(_mapper.Map<DomainCategory>(value));
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _catalogService.Delete<DomainCategory>(id);
    }
}
