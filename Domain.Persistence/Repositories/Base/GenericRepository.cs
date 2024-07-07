
using Domain.Application.Abstractions.Interface.Repositories;
using Domain.Domain.Entities;
using Domain.Domain.Exceptions;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace Domain.Persistence.Repositories.Base;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>, IDisposable
        where TEntity : BaseEntities<TKey>
{
        private readonly ApplicationDBContext _context;

    public GenericRepository(ApplicationDBContext context)
        => _context = context;

    public void Dispose()
        => _context?.Dispose();

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }
    public IQueryable<TResult> FindSelectAll<TResult>(Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>> selector=null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);
        if (predicate is not null)
            items = items.Where(predicate);
        return items.Select(selector);
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);

    public async Task<bool> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();  
        return true;
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entity)
    {
        await _context.Set<TEntity>().AddRangeAsync(entity);
        await _context.SaveChangesAsync();
    }


    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
         _context.SaveChanges();
    }

    public void RemoveMultiple(List<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        _context.SaveChanges();
    }
     

    public async void UpdateMany(IEnumerable<TEntity> entity)
    {
        _context.Set<TEntity>().UpdateRange(entity);
        await _context.SaveChangesAsync();  
    }
    public async Task<bool> Update(TEntity entity)
    {
        return await UpdateWithObject(entity, "CreateDate", "CreateBy");
    }
    private async Task<bool> UpdateWithObject(TEntity pObj, params string[] pNotUpdatedProperties)
    {
        var keyNames = GetPrimaryKey();
        var keyValues = keyNames.Select(p => pObj.GetType().GetProperty(p)?.GetValue(pObj, null)).ToArray();
        if (keyValues != null)
        {
            TEntity exist = GetObject(keyValues!)!;
            if (exist != null)
            {
                _context.Set<TEntity>().Update(pObj);
                var entry = _context.Entry(pObj);
                if (pNotUpdatedProperties.Any())
                {
                    foreach (string prop in pNotUpdatedProperties)
                    {
                        entry.Property(prop).IsModified = false;
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
    }
    private string[] GetPrimaryKey()
    {
        return _context.Model?.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties
            .Select(x => x.Name)?.ToArray() ?? Array.Empty<string>();
    }
    public TEntity? GetObject(params object[] pKeys)
    {
        return _context.Set<TEntity>().Find(pKeys);
    }
}

