using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain.Entities;
using DalItem = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Item;
using DalCategory = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Category;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class ItemRepository : GenericRepository<Item, DalItem>
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

    //public override void Update(Item category)
    //{
    //    var dalCategory = Mapper.Map<DalItem>(category);
    //    if (dalCategory.Parent == null)
    //    {
    //        base.Update(category);
    //        return;
    //    }

    //    var parentFromDb = Entities.FirstOrDefault(c => c.Id == dalCategory.Parent.Id);
    //    dalCategory.Parent = parentFromDb;

    //    base.Update(dalCategory);
    //}
}
