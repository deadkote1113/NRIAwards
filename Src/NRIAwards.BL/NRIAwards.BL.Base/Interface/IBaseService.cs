namespace NRIAwards.BL.Base.Interface;

public interface IBaseService<TEntity, TId, TSearchParams, TOrderParams, TIncludeParams>
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
    where TSearchParams : BaseSearchParams, new()
    where TOrderParams : BaseOrderParams, new()
    where TIncludeParams : class
{
    Task<TId> AddOrUpdateAsync(TEntity entity);
    Task<IList<TId>> AddOrUpdateAsync(IList<TEntity> entities);
    Task<bool> DeleteAsync(TId id);
    Task<bool> ExistsAsync(TId id);
    Task<bool> ExistsAsync(TSearchParams searchParams);
    Task<TEntity> GetAsync(TId id, TIncludeParams? convertParams = null);
    Task<SearchResult<TEntity>> GetAsync(TSearchParams searchParams, TOrderParams? orderParams = null, TIncludeParams? includeParams = null);
    Task<TEntity> GetFirstAsync(TSearchParams searchParams, TOrderParams? orderParams = null, TIncludeParams? includeParams = null);
    Task<IList<TEntity>> GetAll(TIncludeParams? includeParams = null);
}