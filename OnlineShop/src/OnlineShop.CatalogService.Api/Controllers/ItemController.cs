﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Api.Entities;
using OnlineShop.CatalogService.Api.Links;
using OnlineShop.CatalogService.Api.Models;
using OnlineShop.CatalogService.Api.Pagination;
using OnlineShop.CatalogService.Domain;
using OnlineShop.Identity.Core;
using DomainItem = OnlineShop.CatalogService.Domain.Entities.Item;

namespace OnlineShop.CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 5;

        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [Route("/api/Items/Page/{pageNumber}")]
        [HttpGet]
        public GetItemsResponse GetPage(int pageNumber)
        {
            var (from, to) = PageCalculator.CalcIndexRange(pageNumber, _pageSize);
            var range = _itemService.GetRange(from, to);
            var page = new Page<Item>(
                range.Entities.Select(di => _mapper.Map<Item>(di)).ToList(),
                range.TotalCount,
                pageNumber,
                _pageSize);

            return new GetItemsResponse
            {
                Page = page,
                Links = ItemsLinksFactory.Create(Request, page)
            };
        }

        [Authorize(nameof(ApplicationPolicies.ReadAllowed))]
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return _mapper.Map<Item>(_itemService.Get(id));
        }

        [Authorize(nameof(ApplicationPolicies.CreateAllowed))]
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            var item = _mapper.Map<DomainItem>(value);
            _itemService.Add(item);
        }

        [Authorize(nameof(ApplicationPolicies.UpdateAllowed))]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            _itemService.Update(_mapper.Map<DomainItem>(value));
        }

        [Authorize(nameof(ApplicationPolicies.DeleteAllowed))]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _itemService.Delete(id);
        }
    }
}
