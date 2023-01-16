using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.Infrastructure.Adapters;
using OnlineShop.CatalogService.Infrastructure.DAL;
using OnlineShop.CatalogService.Api.MappingProfiles;
using OnlineShop.Identity.Core;
using OnlineShop.Messaging.Abstraction;
using OnlineShop.Messaging.Abstraction.Entities;
using OnlineShop.Messaging.Service;
using OnlineShop.Messaging.Service.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(CreateMapper);
builder.Services.AddSingleton(GetSettings);
builder.Services.AddScoped<DbContext>(sp => new CatalogContext());
builder.Services.AddScoped<IConnectionProvider, ConnectionProvider>();
builder.Services.AddScoped<IPublisher<ItemChangedMessage>, Publisher<ItemChangedMessage>>();
builder.Services.AddScoped<IBusPublisher<ItemChangedMessage>, BusPublisherAdapter<ItemChangedMessage>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
 
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            ValidateLifetime = true,
 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(builder.Configuration["JwtOptions:Key"])),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(nameof(ApplicationPolicies.CreateAllowed), ApplicationPolicies.CreateAllowed);
    options.AddPolicy(nameof(ApplicationPolicies.ReadAllowed), ApplicationPolicies.ReadAllowed);
    options.AddPolicy(nameof(ApplicationPolicies.UpdateAllowed), ApplicationPolicies.UpdateAllowed);
    options.AddPolicy(nameof(ApplicationPolicies.DeleteAllowed), ApplicationPolicies.DeleteAllowed);
});
    
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
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
