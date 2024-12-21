using Microsoft.EntityFrameworkCore;
using Orderly.Domain.Entities;

namespace Orderly.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            // Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product A",
                    Price = 10.00m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product B",
                    Price = 20.00m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product C",
                    Price = 30.00m
                }
            );

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(12, 2); 

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(12, 2);
        }
    }
}
