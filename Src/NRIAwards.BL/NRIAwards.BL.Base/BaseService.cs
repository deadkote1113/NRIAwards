namespace NRIAwards.BL.Base;

public class BaseService<TEntity, TId, TSearchParams, TOrderParams, TIncludeParams> : IBaseService<TEntity, TId, TSearchParams, TOrderParams, TIncludeParams> where TEntity : BaseEntity<TId>
    where TSearchParams : BaseSearchParams, new()
    where TOrderParams : BaseOrderParams, new()
    where TIncludeParams : class
    where TId : IEquatable<TId>
{
    protected readonly IBaseRepository<TEntity, TId, TSearchParams, TOrderParams, TIncludeParams> _repository;

    protected BaseService(IBaseRepository<TEntity, TId, TSearchParams, TOrderParams, TIncludeParams> repository)
    {
        _repository = repository;
    }

    public async Task<TId> AddOrUpdateAsync(TEntity entity)
    {
        if (entity.CreatedAt == DateTime.MinValue)
        {
            entity.CreatedAt = Helpers.GetCurrentDate();
        }
        entity.UpdatedAt = Helpers.GetCurrentDate();
        entity.Id = await _repository.AddOrUpdateAsync(entity);
        return entity.Id;
    }

    public async Task<IList<TId>> AddOrUpdateAsync(IList<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreatedAt == DateTime.MinValue)
            {
                entity.CreatedAt = Helpers.GetCurrentDate();
            }
            entity.UpdatedAt = Helpers.GetCurrentDate();
        }
        var result = await _repository.AddOrUpdateAsync(entities);
        return result;
    }

    public Task<bool> DeleteAsync(TId id)
    {
        return _repository.DeleteAsync(id);
    }

    public Task<bool> ExistsAsync(TId id)
    {
        return _repository.ExistsAsync(id);
    }

    public Task<bool> ExistsAsync(TSearchParams searchParams)
    {
        return _repository.ExistsAsync(searchParams);
    }

    public async Task<TEntity> GetAsync(TId id, TIncludeParams? convertParams = null)
    {
        var entity = await _repository.GetAsync(id, convertParams);
        return entity;
    }

    public async Task<SearchResult<TEntity>> GetAsync(TSearchParams searchParams, TOrderParams? orderParams = null, TIncludeParams? includeParams = null)
    {
        orderParams ??= new TOrderParams();
        var entities = await _repository.GetAsync(searchParams, orderParams, includeParams);
        return entities;
    }

    public async Task<TEntity> GetFirstAsync(TSearchParams searchParams, TOrderParams? orderParams = null, TIncludeParams? includeParams = null)
    {
        var entites = await GetAsync(searchParams, orderParams, includeParams);
        return entites.Objects.FirstOrDefault();
    }

    public async Task<IList<TEntity>> GetAll(TIncludeParams? includeParams = null)
    {
        var entities = await _repository.GetAsync(new(), new(), includeParams);
        return entities.Objects;
    }
}
