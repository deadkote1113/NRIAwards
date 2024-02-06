using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NRIAwards.Common.Entity;
using NRIAwards.Common.Entity.Order;
using NRIAwards.Common.Entity.Search;
using NRIAwards.Common.Heplers;
using System.Linq.Expressions;

namespace NRIAwards.DAL.Base;

public abstract class BaseRepository<TDbContext, TDbObject, TEntity, TId, TSearchParams, TOrderParams, TIncludeParams>
    where TDbContext : DbContext
    where TDbObject : BaseModel<TId>, new()
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
    where TSearchParams : BaseSearchParams
    where TOrderParams : BaseOrderParams
    where TIncludeParams : class
{
    protected TDbContext _context;

    protected abstract bool RequiresUpdatesAfterObjectSaving { get; }

    protected BaseRepository(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<TDbContext>();
    }

    public virtual async Task<TId> AddOrUpdateAsync(TEntity entity)
    {
        var objects = _context.Set<TDbObject>();
        var dbObject = await objects.FirstOrDefaultAsync(item => item.Id.Equals(entity.Id));
        var exists = dbObject != null;
        if (!exists)
        {
            dbObject = new TDbObject();
        }
        await UpdateBeforeSavingAsync(entity, dbObject, exists);
        if (!exists)
        {
            objects.Add(dbObject);
        }
        if (RequiresUpdatesAfterObjectSaving)
        {
            await _context.SaveChangesAsync();
            await UpdateAfterSavingAsync(entity, dbObject, exists);
        }
        await _context.SaveChangesAsync();
        return dbObject.Id;
    }

    public virtual async Task<IList<TId>> AddOrUpdateAsync(IList<TEntity> entities)
    {
        var entitiesIdArray = entities.Select(item => item.Id).ToArray();
        var dbSet = _context.Set<TDbObject>();
        var dbObjectsDictionary = await dbSet.Where(item => entitiesIdArray.Any(id => item.Id.Equals(id))).ToDictionaryAsync(item => item.Id);
        var existingSet = new HashSet<TId>();
        var dbObjects = new List<TDbObject>();
        var addedObjects = new List<TDbObject>();
        foreach (var entity in entities)
        {
            var id = entity.Id;
            var exists = dbObjectsDictionary.ContainsKey(id);
            var dbObject = exists ? dbObjectsDictionary[id] : new TDbObject();
            if (exists)
                existingSet.Add(id);
            await UpdateBeforeSavingAsync(entity, dbObject, exists);
            dbObjects.Add(dbObject);
            if (!exists)
            {
                addedObjects.Add(dbObject);
            }
        }
        dbSet.AddRange(addedObjects);
        if (RequiresUpdatesAfterObjectSaving)
        {
            await _context.SaveChangesAsync();
            for (var i = 0; i < dbObjects.Count; i++)
            {
                var dbObject = dbObjects[i];
                var id = dbObject.Id;
                await UpdateAfterSavingAsync(entities[i], dbObject, existingSet.Contains(id));
            }
        }
        await _context.SaveChangesAsync();
        return dbObjects.Select(item => item.Id).ToList();
    }

    public virtual async Task<TEntity> GetAsync(TId id, TIncludeParams convertParams)
    {
        var dbObjects = _context.Set<TDbObject>().Where(item => item.Id.Equals(id)).Take(1);
        return (await BuildEntitiesListAsync(dbObjects, convertParams)).FirstOrDefault();
    }

    public virtual Task<bool> ExistsAsync(TId id)
    {
        return _context.Set<TDbObject>().Where(item => item.Id.Equals(id)).AnyAsync();
    }

    public virtual async Task<bool> ExistsAsync(TSearchParams searchParams)
    {
        var objects = _context.Set<TDbObject>().AsNoTracking();
        return await (await BuildDbQueryAsync(objects, searchParams)).AnyAsync();
    }

    public virtual async Task<bool> DeleteAsync(TId id)
    {
        var result = await _context.Set<TDbObject>().FirstOrDefaultAsync(p => p.Id.Equals(id));
        if (result == null)
        {
            return false;
        }
        result.DeletedAt = Helpers.GetCurrentDate();
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task DeleteManyAsync(Expression<Func<TDbObject, bool>> predicate)
    {
        var dbObjects = await _context.Set<TDbObject>().Where(predicate).ToListAsync();
        foreach (var dbObject in dbObjects)
        {
            dbObject.DeletedAt = Helpers.GetCurrentDate();
        }
        await _context.SaveChangesAsync();
    }

    public virtual async Task<SearchResult<TEntity>> GetAsync(TSearchParams searchParams, TOrderParams orderParams, TIncludeParams convertParams)
    {
        var objects = _context.Set<TDbObject>().AsNoTracking();
        objects = await BuildDbQueryAsync(objects, searchParams);
        objects = await OrderDbQueryAsync(objects, orderParams);
        var result = new SearchResult<TEntity>
        {
            Total = await objects.CountAsync(),
            RequestedObjectsCount = searchParams.ObjectsCount,
            RequestedStartIndex = searchParams.StartIndex,
            Objects = new List<TEntity>()
        };
        if (searchParams.ObjectsCount == 0)
        {
            return result;
        }
        objects = objects.Skip(searchParams.StartIndex);
        if (searchParams.ObjectsCount != null)
        {
            objects = objects.Take(searchParams.ObjectsCount.Value);
        }
        result.Objects = await BuildEntitiesListAsync(objects, convertParams);
        return result;
    }

    internal virtual async Task<IList<TEntity>> GetAsync(Expression<Func<TDbObject, bool>> predicate, TIncludeParams convertParams = null)
    {
        return await BuildEntitiesListAsync(_context.Set<TDbObject>().Where(predicate), convertParams);
    }

    protected virtual async Task UpdateBeforeSavingAsync(TEntity entity, TDbObject dbObject, bool exists)
    {
        dbObject.CreatedAt = entity.CreatedAt;
        dbObject.UpdatedAt = entity.UpdatedAt;
        dbObject.DeletedAt = entity.DeletedAt;
    }

    protected virtual Task UpdateAfterSavingAsync(TEntity entity, TDbObject dbObject, bool exists)
    {
        return Task.CompletedTask;
    }

    protected virtual async Task<IQueryable<TDbObject>> BuildDbQueryAsync(IQueryable<TDbObject> dbObjects, TSearchParams searchParams)
    {
        dbObjects = dbObjects.Where(item => searchParams.ExcludeDeleted == true && item.DeletedAt == null || searchParams.ExcludeDeleted == false);
        return dbObjects;
    }

    protected virtual async Task<IQueryable<TDbObject>> OrderDbQueryAsync(IQueryable<TDbObject> dbObjects, TOrderParams orderParams)
    {
        if (orderParams.OrderByIdAsc.HasValue)
        {
            dbObjects = orderParams.OrderByIdAsc.Value ? dbObjects.OrderBy(item => item.Id) : dbObjects.OrderByDescending(item => item.Id);
        }
        return dbObjects;
    }

    protected abstract Task<IList<TEntity>> BuildEntitiesListAsync(IQueryable<TDbObject> dbObjects, TIncludeParams includeParams);
}
