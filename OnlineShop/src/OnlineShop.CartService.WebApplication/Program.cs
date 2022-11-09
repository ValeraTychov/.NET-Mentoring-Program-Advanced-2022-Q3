using AutoMapper;
using OnlineShop.CartService.BLL;
using OnlineShop.CartService.DAL;
using OnlineShop.CartService.WebApplication.Controllers;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMapper>(CreateMapper);
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<CartController>();

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
        cfg.AddProfile<OnlineShop.CartService.WebApplication.MappingProfiles.CartServiceProfile>();
        cfg.AddProfile<OnlineShop.CartService.BLL.MappingProfiles.CartServiceProfile>();
    });

    return mapperConfiguration.CreateMapper();
}