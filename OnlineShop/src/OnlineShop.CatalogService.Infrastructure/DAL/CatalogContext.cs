using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Infrastructure.DAL.Entities;

namespace OnlineShop.CatalogService.Infrastructure.DAL;

public class CatalogContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=OnlineShop.Catalog;Trusted_Connection=True");
    }
}
