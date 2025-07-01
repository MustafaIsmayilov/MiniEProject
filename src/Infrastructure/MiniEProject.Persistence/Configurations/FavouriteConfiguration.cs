using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Configurations;

public class FavouriteConfiguration : IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.HasKey(f => f.Id);

        builder.HasOne(f => f.User)
               .WithMany(u => u.Favourites)
               .HasForeignKey(f => f.UserId);

        builder.HasOne(f => f.Product)
               .WithMany(p => p.Favourites)
               .HasForeignKey(f => f.ProductId);

        builder.HasIndex(f => new { f.UserId, f.ProductId }).IsUnique();
    }
}
