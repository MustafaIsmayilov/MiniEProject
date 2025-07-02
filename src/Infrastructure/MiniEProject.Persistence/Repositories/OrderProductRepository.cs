using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;

namespace MiniEProject.Persistence.Repositories;

public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(MiniEProjectDbContext context) : base(context)
    {
    }
}
