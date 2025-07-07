using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Name).IsRequired().HasMaxLength(40);
        builder.Property(pc => pc.Description).HasMaxLength(100);
        builder.Property(pc => pc.Price).HasColumnType("decimal(18,2)");
        builder.Property(pc => pc.Stock).IsRequired();
        builder.Property(pc => pc.Condition).HasConversion<string>();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Price)
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);

        builder.HasOne(p => p.User)
         .WithMany(u => u.Products)
         .HasForeignKey(p => p.UserId)
         .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ProductImages)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.Favourites)
         .WithOne(x => x.Product)
         .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.Reviews)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasMany(x => x.OrderProducts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}
