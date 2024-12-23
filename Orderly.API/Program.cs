using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Orderly.Application.CQRS.Commands.Order;
using Orderly.Application.CQRS.Handlers.Order;
using Orderly.Application.Services.Order;
using Orderly.Domain.Interfaces.Messaging.Kafka;
using Orderly.Domain.Interfaces.Repositories.Customer;
using Orderly.Domain.Interfaces.Repositories.Order;
using Orderly.Domain.Interfaces.Repositories.OrderItem;
using Orderly.Domain.Interfaces.Repositories.Product;
using Orderly.Domain.Interfaces.Services.Order;
using Orderly.Infrastructure.Messaging.Kafka.Producers;
using Orderly.Infrastructure.Persistence;
using Orderly.Infrastructure.Repositories.Customer;
using Orderly.Infrastructure.Repositories.Order;
using Orderly.Infrastructure.Repositories.OrderItem;
using Orderly.Infrastructure.Repositories.Product;
using Orderly.Infrastructure.Settings;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orderly API", Version = "v1" });
    c.EnableAnnotations();
});

// Add services to the container.
//SQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

//MongoDB
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
builder.Services.AddScoped<MongoDbContext>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration["MongoDbSettings:ConnectionString"];
    var databaseName = configuration["MongoDbSettings:DatabaseName"];

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentNullException(nameof(connectionString), "MongoDB connection string cannot be null or empty.");
    }

    return new MongoDbContext(connectionString, databaseName);
});



builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
builder.Services.AddScoped<IOrderItemCommandRepository, OrderItemCommandRepository>();
builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();


builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();

//builder.Services.AddMediatR(typeof(Program).Assembly);


// Configuração do Kafka
builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new ProducerConfig
    {
        BootstrapServers = configuration["Kafka:BootstrapServers"] // Certifique-se de que este valor está definido no appsettings.json
    };
});

builder.Services.AddSingleton<IOrderEventProducer, OrderEventProducer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly));

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
