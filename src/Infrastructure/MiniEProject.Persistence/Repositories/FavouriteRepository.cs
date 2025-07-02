using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;

namespace MiniEProject.Persistence.Repositories;

public class FavouriteRepository : GenericRepository<Favourite>, IFavouriteRepository
{
    public FavouriteRepository(MiniEProjectDbContext context) : base(context)
    {
    }
}