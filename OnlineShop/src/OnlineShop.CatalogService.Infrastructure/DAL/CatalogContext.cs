using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Infrastructure.DAL.Entities;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class CatalogContext : DbContext
{
    public CatalogContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Data Source=EPPLWROW0400\SQLEXPRESS;Initial Catalog=OnlineShop.Catalog;user id=sa;password=omega9999#;;Connect Timeout=30;");
    }
}
