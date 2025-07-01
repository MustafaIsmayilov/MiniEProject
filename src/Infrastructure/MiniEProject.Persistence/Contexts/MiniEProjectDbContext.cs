using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Persistence.Contexts;

public class MiniEProjectDbContext : IdentityDbContext<AppUser>
{
    public MiniEProjectDbContext(DbContextOptions<MiniEProjectDbContext> options):base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Product> Products { get; set; }
}
