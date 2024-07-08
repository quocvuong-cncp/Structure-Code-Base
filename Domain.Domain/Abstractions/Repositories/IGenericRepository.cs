using System.Linq.Expressions;
using Domain.Domain.Entities;

namespace Domain.Domain.Abstractions.Interface.Repositories;

public interface IGenericRepository<TEntity, in TKey> where TEntity :  BaseEntities<TKey>
{
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
    public IQueryable<TResult> FindSelectAll<TResult>(Expression<Func<TEntity, bool>>? predicate = null,
    Expression<Func<TEntity, TResult>> selector = null,
    params Expression<Func<TEntity, object>>[] includeProperties);
    public Task<bool> AddAsync(TEntity entity);
    public Task AddManyAsync(IEnumerable<TEntity> entity);
    public void UpdateMany(IEnumerable<TEntity> entity);
    public Task<bool> Update(TEntity entity);
    void Remove(TEntity entity);

    void RemoveMultiple(List<TEntity> entities);
}
