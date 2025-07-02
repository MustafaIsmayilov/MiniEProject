using System.Linq.Expressions;
using MiniEProject.Domain.Entities;

namespace MiniEProject.Application.Abstracts.Repositories;

public interface IRepository<T> where T : BaseEntity, new()
{
    Task<T?> GetByIdAsync(Guid id);
    IQueryable<T> GetByIdFiltered(Expression<Func<T, bool>>? predicate,
        Expression<Func<T, object>>[]? include = null,
        bool isTracking = false);

    IQueryable<T?> GetAll(bool isTracking = false);

    IQueryable<T?> GetAllFiltered(Expression<Func<T, bool>>? predicate,
        Expression<Func<T, object>>[]? include = null,
        Expression<Func<T, object>>? OrderBy = null,
        bool isOrderByAsc = true,
        bool isTracking = false);

    

    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangeAsync();
}
