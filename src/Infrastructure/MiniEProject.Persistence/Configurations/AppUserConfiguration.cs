using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(us => us.FullName).
            IsRequired().
            HasMaxLength(50);
        builder.Property(us => us.Address).
            IsRequired().
            HasMaxLength(255);
    }
}
