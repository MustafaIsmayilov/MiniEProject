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

        builder.HasOne(pc => pc.User)
               .WithMany(u => u.Products)
               .HasForeignKey(p => p.UserId);

        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);
    }
}
