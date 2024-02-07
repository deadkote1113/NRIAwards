namespace NRIAwards.DAL.Base.Interface;

public interface IBaseRepository<TEntity, TId, TSearchParams, TOrderParams, TIncludeParams>
    where TEntity : BaseEntity<TId>
    where TId : IEquatable<TId>
    where TSearchParams : BaseSearchParams
    where TOrderParams : BaseOrderParams
    where TIncludeParams : class
{
    Task<TId> AddOrUpdateAsync(TEntity entity);
    Task<IList<TId>> AddOrUpdateAsync(IList<TEntity> entities);
    Task<TEntity> GetAsync(TId id, TIncludeParams? convertParams);
    Task<SearchResult<TEntity>> GetAsync(TSearchParams searchParams, TOrderParams orderParams, TIncludeParams? convertParams);
    Task<bool> ExistsAsync(TId id);
    Task<bool> ExistsAsync(TSearchParams searchParams);
    Task<bool> DeleteAsync(TId id);
}