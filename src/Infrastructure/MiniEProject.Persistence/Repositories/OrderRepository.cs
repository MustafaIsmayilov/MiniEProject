using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;

namespace MiniEProject.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(MiniEProjectDbContext context) :base(context)
    {
       
    }
}
