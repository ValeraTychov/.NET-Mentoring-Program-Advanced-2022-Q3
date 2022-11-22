using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Domain.Entities;
using DalCategory = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Category;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class CategoryRepository : GenericRepository<Category, DalCategory>, ICategoryRepository
{
    public CategoryRepository(IMapper mapper, DbContext dbContext) : base(mapper, dbContext)
    {
    }

    public override void Add(Category category)
    {
        var dalCategory = Mapper.Map<DalCategory>(category);

        if (dalCategory.Parent == null)
        {
            base.Add(dalCategory);
        }
                
        Add(dalCategory);
    }

    private new void Add(DalCategory dalCategory)
    {
        var parentFromDb = Entities.FirstOrDefault(c => c.Id == dalCategory.Parent.Id);

        if (parentFromDb == null)
        {
            return;
        }

        dalCategory.Parent = parentFromDb;
        base.Add(dalCategory);
    }

    public override void Update(Category item)
    {
        var dalCategory = Mapper.Map<DalCategory>(item);
        if (dalCategory.Parent == null)
        {
            base.Update(item);
            return;
        }
        
        var parentFromDb = Entities.FirstOrDefault(c => c.Id == dalCategory.Parent.Id);
        dalCategory.Parent = parentFromDb;

        base.Update(dalCategory);
    }

    public IEnumerable<Category> GetRange(int from = 0, int to = int.MaxValue)
    {
        var count = to - from + 1;
        return Entities.Skip(from).Take(count).AsEnumerable().Select(Mapper.Map<Category>);
    }

    public IEnumerable<Category> GetChildren(int parentId)
    {
        return Entities.Where(c => c.Parent.Id == parentId).AsEnumerable().Select(Mapper.Map<Category>);
    }

    public int GetCount()
    {
        return Entities.Count();
    }
}
