using AutoMapper;
using OnlineShop.CatalogService.Domain;
using OnlineShop.CatalogService.WebApplication.DI;
using OnlineShop.CatalogService.WebApplication.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMapper>(sp => new MapperConfiguration(cfg => cfg.AddProfile<CatalogServiceProfile>()).CreateMapper());
builder.Services.AddScoped<ICatalogService, CatalogService>(sp => CatalogServiceFactory.Create());
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
