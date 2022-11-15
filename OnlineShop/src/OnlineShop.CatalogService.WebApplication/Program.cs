using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Infrastructure.DAL;
using OnlineShop.CatalogService.WebApplication.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(CreateMapper);
builder.Services.AddScoped<DbContext>(sp => new CatalogContext());
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

IMapper CreateMapper(IServiceProvider serviceProvider)
{
    var mapperConfiguration = new MapperConfiguration(
        cfg =>
        {
            cfg.AddProfile<OnlineShop.CatalogService.Infrastructure.MappingProfiles.CatalogServiceProfile>();
            cfg.AddProfile<CatalogServiceProfile>();
        });

    return mapperConfiguration.CreateMapper();
}