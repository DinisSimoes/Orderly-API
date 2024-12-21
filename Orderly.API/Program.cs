using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Orderly.Domain.Interfaces.Repositories.Customer;
using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Domain.Interfaces.Repositories.OrderItem;
using Orderly.Domain.Interfaces.Repositories.Product;
using Orderly.Infrastructure.Persistence;
using Orderly.Infrastructure.Repositories.Customer;
using Orderly.Infrastructure.Repositories.Order;
using Orderly.Infrastructure.Repositories.OrderItem;
using Orderly.Infrastructure.Repositories.Product;
using Orderly.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//SQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
builder.Services.AddScoped<IOrderItemCommandRepository, OrderItemCommandRepository>();
builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();

//MongoDB
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongoDbSettings.ConnectionString));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbSettings.DatabaseName));

//builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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
