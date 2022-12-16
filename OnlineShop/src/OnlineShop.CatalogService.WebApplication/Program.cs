using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Infrastructure.Adapters;
using OnlineShop.CatalogService.Infrastructure.DAL;
using OnlineShop.CatalogService.WebApplication.MappingProfiles;
using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;
using OnlineShop.Messaging.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(CreateMapper);
builder.Services.AddSingleton(GetSettings);
builder.Services.AddScoped<DbContext>(sp => new CatalogContext());
builder.Services.AddSingleton<IPublisher<ItemChangedMessage>, Publisher<ItemChangedMessage>>();
builder.Services.AddSingleton<IBusPublisher<ItemChangedMessage>, BusPublisherAdapter<ItemChangedMessage>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

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

MessageBrokerSettings GetSettings(IServiceProvider serviceProvider)
{
    return new MessageBrokerSettings
    {
        Host = "localhost",
        Username = "planck",
        Password = "planck",
    };
}
