using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;
using System.Linq.Expressions;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    private readonly MiniEProjectDbContext _context;
    private readonly DbSet<T> _table;

    public GenericRepository(MiniEProjectDbContext context)
    {
        _context = context;
        _table = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _table.FirstOrDefaultAsync(e => e.Id == id);
    }

    public IQueryable<T> GetByFiltered(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>[]? include = null,
        bool isTracking = false)
    {
        IQueryable<T> query = _table;

        if (!isTracking)
            query = query.AsNoTracking();

        if (include is not null)
        {
            foreach (var item in include)
                query = query.Include(item);
        }

        return query.Where(predicate);
    }

    public IQueryable<T> GetAll(bool isTracking = false)
    {
        return isTracking ? _table : _table.AsNoTracking();
    }

    public IQueryable<T?> GetAllFiltered(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>[]? include = null,
        Expression<Func<T, object>>? orderBy = null,
        bool isOrderByAsc = true,
        bool isTracking = false)
    {
        IQueryable<T> query = _table;

        if (!isTracking)
            query = query.AsNoTracking();

        if (include is not null)
        {
            foreach (var item in include)
                query = query.Include(item);
        }

        query = query.Where(predicate);

        if (orderBy is not null)
        {
            query = isOrderByAsc
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
        }

        return query;
    }

    public async Task AddAsync(T entity)
    {
        await _table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _table.Remove(entity);
    }

    public void Update(T entity)
    {
        _table.Update(entity);
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}

