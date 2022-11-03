using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain.Entities;
using DalCategory = OnlineShop.CatalogService.Infrastructure.DAL.Entities.Category;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class CategoryRepository : GenericRepository<Category, DalCategory>
{
    public CategoryRepository(IMapper mapper, DbContext dbContext) : base(mapper, dbContext)
    {
    }

    public override void Add(Category entity)
    {
        var dalCategory = Mapper.Map<DalCategory>(entity);

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

    public override void Update(Category category)
    {
        var dalCategory = Mapper.Map<DalCategory>(category);
        if (dalCategory.Parent == null)
        {
            base.Update(category);
            return;
        }
        
        var parentFromDb = Entities.FirstOrDefault(c => c.Id == dalCategory.Parent.Id);
        dalCategory.Parent = parentFromDb;

        base.Update(dalCategory);
    }
}
