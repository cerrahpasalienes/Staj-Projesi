using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ProductCode).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        
        // Fiyat alanı veritabanında ondalıklı sayı olarak (18 basamak, 2 küsurat) tutulacak
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
    }
}