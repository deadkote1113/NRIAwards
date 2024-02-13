using AwardEventEntity = NRIAwards.Common.Entity.AwardEvent;

namespace NRIAwards.DAL.Repository;

public class AwardEventsRepository : BaseRepository<PostgresDbContext, AwardEvent, AwardEventEntity, int, AwardEventsSearchParams, AwardEventsOrderParams, AwardEventsIncludeParams>,
	ICrudAwardEventsRepository, IExtendedAwardEventsRepository
{

	protected internal AwardEventsRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(AwardEventEntity entity, AwardEvent dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.Title = entity.Title;
		dbObject.Description = entity.Description;
		dbObject.AwardsId = entity.AwardsId;
		dbObject.OrderId = entity.OrderId;
		dbObject.VisualContentId = entity.VisualContentId;
	}
	
	protected override IQueryable<AwardEvent> BuildDbQuery(IQueryable<AwardEvent> dbObjects, AwardEventsSearchParams searchParams)
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
			if(string.IsNullOrEmpty(searchParams.DescriptionLike) == false)
			{
				dbObjects = dbObjects.Where(item => item.Description.ToLower().Contains(searchParams.DescriptionLike.ToLower()));
			}
			if(searchParams.AwardsId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.AwardsId == searchParams.AwardsId);
			}
			if(searchParams.OrderId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.OrderId == searchParams.OrderId);
			}
			if(searchParams.VisualContentId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.VisualContentId == searchParams.VisualContentId);
			}
        }
		return dbObjects;
	}

	protected override IQueryable<AwardEvent> OrderDbQuery(IQueryable<AwardEvent> dbObjects, AwardEventsOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<AwardEventEntity>> BuildEntitiesListAsync(IQueryable<AwardEvent> dbObjects, AwardEventsIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static AwardEventEntity ConvertDbObjectToEntity(AwardEvent dbObject)
	{
		return new AwardEventEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt,
			dbObject.Title, dbObject.Description, dbObject.AwardsId, dbObject.OrderId, dbObject.VisualContentId);
	}
}

