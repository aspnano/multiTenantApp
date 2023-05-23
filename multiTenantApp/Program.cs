using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using multiTenantApp.Middleware;
using multiTenantApp.Models;
using multiTenantApp.Services;
using multiTenantApp.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// adding a database service with configuration -- connection string read from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<TenantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Current tenant service with scoped lifetime (created per each request)
builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();

// Product CRUD service with transient lifetime
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();
