using AutoMapper;
using Microsoft.OpenApi.Models;
using OnlineShop.CartService.BLL;
using OnlineShop.CartService.DAL;
using OnlineShop.CartService.WebApplication;
using OnlineShop.CartService.WebApplication.Controllers.V2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new GroupingByNamespaceConvention());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    var titleBase = "Carting API";

    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = titleBase + " v1",
    });

    config.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = titleBase + " v2",
    });
});

builder.Services.AddScoped<IMapper>(CreateMapper);
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<CartController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "Carting API v1");
        config.SwaggerEndpoint("/swagger/v2/swagger.json", "Carting API v2");
    });
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
        cfg.AddProfile<OnlineShop.CartService.WebApplication.MappingProfiles.CartServiceProfile>();
        cfg.AddProfile<OnlineShop.CartService.BLL.MappingProfiles.CartServiceProfile>();
    });

    return mapperConfiguration.CreateMapper();
}