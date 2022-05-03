using NorthWindAPI.Models;
using NorthWindAPI.Services.Implementations;
using NorthWindAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService> ();

builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddRouting();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseHttpsRedirection();
app.MapDefaultControllerRoute();

app.Run();
