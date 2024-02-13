using VisualContentEntity = NRIAwards.Common.Entity.VisualContent;

namespace NRIAwards.DAL.Repository;

public class VisualContentsRepository : BaseRepository<PostgresDbContext, VisualContent, VisualContentEntity, int, VisualContentsSearchParams, VisualContentsOrderParams, VisualContentsIncludeParams>,
	ICrudVisualContentsRepository, IExtendedVisualContentsRepository
{

	protected internal VisualContentsRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(VisualContentEntity entity, VisualContent dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.Type = entity.Type;
		dbObject.Title = entity.Title;
		dbObject.Link = entity.Link;
	}
	
	protected override IQueryable<VisualContent> BuildDbQuery(IQueryable<VisualContent> dbObjects, VisualContentsSearchParams searchParams)
	{
		if (searchParams != null)
		{
			dbObjects = base.BuildDbQuery(dbObjects, searchParams);
			if (searchParams.Ids != null)
			{
				dbObjects = dbObjects.Where(item => searchParams.Ids.Contains(item.Id));
			}
			if(string.IsNullOrEmpty(searchParams.TitleLike) == false)
			{
				dbObjects = dbObjects.Where(item => item.Title.ToLower().Contains(searchParams.TitleLike.ToLower()));
			}
			if(string.IsNullOrEmpty(searchParams.LinkLike) == false)
			{
				dbObjects = dbObjects.Where(item => item.Link.ToLower().Contains(searchParams.LinkLike.ToLower()));
			}
        }
		return dbObjects;
	}

	protected override IQueryable<VisualContent> OrderDbQuery(IQueryable<VisualContent> dbObjects, VisualContentsOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<VisualContentEntity>> BuildEntitiesListAsync(IQueryable<VisualContent> dbObjects, VisualContentsIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static VisualContentEntity ConvertDbObjectToEntity(VisualContent dbObject)
	{
		return new VisualContentEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt,
			dbObject.Type, dbObject.Title, dbObject.Link);
	}
}

