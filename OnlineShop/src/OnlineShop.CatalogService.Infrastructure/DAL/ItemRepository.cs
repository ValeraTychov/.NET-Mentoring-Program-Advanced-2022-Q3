using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Domain.Entities;
using DalItem = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Item;
using DalCategory = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Category;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class ItemRepository : GenericRepository<Item, DalItem>, IItemRepository
{
    public ItemRepository(IMapper mapper, DbContext dbContext) : base(mapper, dbContext)
    {
    }

    public override void Add(Item item)
    {
        var dalItem = Mapper.Map<DalItem>(item);

        if (dalItem.Category == null)
        {
            return;
        }

        Add(dalItem);
    }

    private new void Add(DalItem dalItem)
    {
        var categoryFromDb = DbContext.Set<DalCategory>().FirstOrDefault(c => c.Id == dalItem.CategoryId);

        if (categoryFromDb == null)
        {
            return;
        }

        dalItem.Category = categoryFromDb;
        base.Add(dalItem);
    }

    public override void Update(Item item)
    {
        var dalItem = Mapper.Map<DalItem>(item);
        
        var parentFromDb = DbContext.Set<DalCategory>().FirstOrDefault(c => c.Id == dalItem.CategoryId);
        dalItem.Category = parentFromDb;

        base.Update(dalItem);
    }

    public IEnumerable<Item> GetRange(int from = 0, int to = int.MaxValue)
    {
        var count = to - from;
        return Entities.Skip(from).Take(count).AsEnumerable().Select(Mapper.Map<Item>);
    }

    public IEnumerable<Item> GetByCategory(int categoryId)
    {
        return Entities.Where(c => c.Category.Id == categoryId).AsEnumerable().Select(Mapper.Map<Item>);
    }

    public int GetCount()
    {
        return Entities.Count();
    }
}
