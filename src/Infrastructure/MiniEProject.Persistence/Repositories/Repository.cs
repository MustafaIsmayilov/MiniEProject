using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MiniEProject.Application.Abstracts.Repositories;
using MiniEProject.Domain.Entities;
using MiniEProject.Persistence.Contexts;

namespace MiniEProject.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    private MiniEProjectDbContext _context { get; }
    private DbSet<T> Table { get; }
    public Repository(MiniEProjectDbContext context)
    {
        _context = context;
        Table = _context.Set<T>();
    }
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public IQueryable<T> GetAll(bool isTracking = false)
    {
        if (!isTracking)
            return Table.AsNoTracking();
        return Table;
    }

    public IQueryable<T?> GetAllFiltered(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? include = null, Expression<Func<T, object>>? orderby = null, bool isOrderByAsc = true, bool isTracking = false)
    {
        IQueryable<T> query = Table;

        if (predicate is not null)
            query = query.Where(predicate);

        if (include is not null)
            foreach (var includeExpression in include)
            {
                query = query.Include(includeExpression);
            }

        if (orderby is not null)
        {
            if (isOrderByAsc)
                query = query.OrderBy(orderby);
            else
                query = query.OrderByDescending(orderby);
        }

        if (isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public IQueryable<T> GetByFiltered(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? include = null, bool isTracking = false)
    {
        IQueryable<T> query = Table;
        if (predicate is not null)
            query = query.Where(predicate);

        if (include is not null)
        {
            foreach (var includeExpression in include)
                query = query.Include(includeExpression);
        }

        if (isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Table.FindAsync(id);
    }

    public Task SaveChangeAsync()
    {
        throw new NotImplementedException();
    }

    public async void Update(T entity)
    {
        await _context.SaveChangesAsync();
    }
}
