using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Domain;
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
    private readonly ICategoryService _categoryService;
    private readonly IItemService _itemService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IItemService itemService, IMapper mapper)
    {
        _categoryService = categoryService;
        _itemService = itemService;
        _mapper = mapper;
    }  

    [HttpGet]
    public GetCategoriesResponse Get()
    {
        var categories = _categoryService.GetRange().Select(dc => _mapper.Map<Category>(dc));
        return new GetCategoriesResponse { Categories = categories };
    }

    [HttpGet("{id}")]
    public GetCategoryResponse? Get(int id)
    {
        var category = _mapper.Map<Category>(_categoryService.Get(id));

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

    [Route("{id}/Children")]
    [HttpGet]
    public IEnumerable<Category> GetChildrenCategories(int id)
    {
        return _categoryService.GetChildren(id).Select(c => _mapper.Map<Category>(c));
    }

    [Route("{id}/Items")]
    [HttpGet]
    public IEnumerable<Item> GetItems(int id)
    {
        var items = _itemService
            .GetByCategory(id)
            .Select(i => _mapper.Map<Item>(i));
        return items;
    }

    [HttpPost]
    public void Post([FromBody] Category value)
    {
        _categoryService.Add(_mapper.Map<DomainCategory>(value));
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Category value)
    {
        _categoryService.Update(_mapper.Map<DomainCategory>(value));
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _categoryService.Delete(id);
    }
}
