using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Content).HasMaxLength(1000);
        builder.Property(r => r.Rating).IsRequired();

        builder.HasOne(r => r.User)
               .WithMany(us => us.Reviews)
               .HasForeignKey(r => r.UserId);

        builder.HasOne(r => r.Product)
               .WithMany(p => p.Reviews)
               .HasForeignKey(r => r.ProductId);
    }
}
