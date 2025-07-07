using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FullName)
                 .IsRequired()
                 .HasMaxLength(100);

        builder.Property(u => u.Address)
            .HasMaxLength(200);

        builder.Property(u => u.Age)
            .IsRequired();

        builder.Property(u => u.BirthDate)
            .IsRequired();

        builder.Property(u => u.RefreshToken)
            .HasMaxLength(500); // refresh token stringdir deyə limit qoymaq yaxşıdır

        builder.Property(u => u.RefreshExpireDate)
            .IsRequired();

        // Relation - One to many
        builder.HasMany(u => u.Favourites)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Products)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
