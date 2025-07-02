using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;

namespace MiniEProject.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(MiniEProjectDbContext context) : base(context)
    {
    }
}
