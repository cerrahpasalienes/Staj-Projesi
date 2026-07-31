using Microsoft.EntityFrameworkCore;
using Domain.Entities; // Entity'lerinin bulunduğu klasörün namespace'i (Domain projesindeki ad alanı)

namespace Infrastructure.Data.DataContext;

public class OrderManagementDbContext : DbContext
{
    public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options) : base(options) 
    { 
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configurations klasöründeki tüm IEntityTypeConfiguration sınıflarını otomatik bulur ve uygular
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderManagementDbContext).Assembly);
    }
} // Sınıf burada kapanmalı