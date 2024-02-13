using AwardSessionEntity = NRIAwards.Common.Entity.AwardSession;

namespace NRIAwards.DAL.Repository;

public class AwardSessionsRepository : BaseRepository<PostgresDbContext, AwardSession, AwardSessionEntity, int, AwardSessionsSearchParams, AwardSessionsOrderParams, AwardSessionsIncludeParams>,
	ICrudAwardSessionsRepository, IExtendedAwardSessionsRepository
{

	protected internal AwardSessionsRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(AwardSessionEntity entity, AwardSession dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.UserId = entity.UserId;
		dbObject.ConnectionCode = entity.ConnectionCode;
		dbObject.State = entity.State;
		dbObject.PassedStagesCount = entity.PassedStagesCount;
		dbObject.AwardId = entity.AwardId;
	}
	
	protected override IQueryable<AwardSession> BuildDbQuery(IQueryable<AwardSession> dbObjects, AwardSessionsSearchParams searchParams)
	{
		if (searchParams != null)
		{
			dbObjects = base.BuildDbQuery(dbObjects, searchParams);
			if (searchParams.Ids != null)
			{
				dbObjects = dbObjects.Where(item => searchParams.Ids.Contains(item.Id));
			}
			if(searchParams.UserId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.UserId == searchParams.UserId);
			}
			if(string.IsNullOrEmpty(searchParams.ConnectionCodeLike) == false)
			{
				dbObjects = dbObjects.Where(item => item.ConnectionCode.ToLower().Contains(searchParams.ConnectionCodeLike.ToLower()));
			}
			if(searchParams.State.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.State == searchParams.State);
			}
			if(searchParams.PassedStagesCount.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.PassedStagesCount == searchParams.PassedStagesCount);
			}
			if(searchParams.AwardId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.AwardId == searchParams.AwardId);
			}
        }
		return dbObjects;
	}

	protected override IQueryable<AwardSession> OrderDbQuery(IQueryable<AwardSession> dbObjects, AwardSessionsOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<AwardSessionEntity>> BuildEntitiesListAsync(IQueryable<AwardSession> dbObjects, AwardSessionsIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static AwardSessionEntity ConvertDbObjectToEntity(AwardSession dbObject)
	{
		return new AwardSessionEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt,
			dbObject.UserId, dbObject.ConnectionCode, dbObject.State, dbObject.PassedStagesCount, dbObject.AwardId);
	}
}

