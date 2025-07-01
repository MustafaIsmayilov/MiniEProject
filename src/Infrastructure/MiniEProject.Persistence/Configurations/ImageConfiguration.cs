using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Image_Url).
            IsRequired().
            HasMaxLength(255);

        builder.HasOne(i => i.Product)
               .WithMany(p => p.ProductImages)
               .HasForeignKey(i => i.ProductId);
    }
}
