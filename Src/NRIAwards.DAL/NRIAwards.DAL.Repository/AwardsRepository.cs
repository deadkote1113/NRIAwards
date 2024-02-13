using AwardEntity = NRIAwards.Common.Entity.Award;

namespace NRIAwards.DAL.Repository;

public class AwardsRepository : BaseRepository<PostgresDbContext, Award, AwardEntity, int, AwardsSearchParams, AwardsOrderParams, AwardsIncludeParams>,
	ICrudAwardsRepository, IExtendedAwardsRepository
{

	protected internal AwardsRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(AwardEntity entity, Award dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.Title = entity.Title;
		dbObject.Description = entity.Description;
		dbObject.VisualContentId = entity.VisualContentId;
	}
	
	protected override IQueryable<Award> BuildDbQuery(IQueryable<Award> dbObjects, AwardsSearchParams searchParams)
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
			if(searchParams.VisualContentId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.VisualContentId == searchParams.VisualContentId);
			}
        }
		return dbObjects;
	}

	protected override IQueryable<Award> OrderDbQuery(IQueryable<Award> dbObjects, AwardsOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<AwardEntity>> BuildEntitiesListAsync(IQueryable<Award> dbObjects, AwardsIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static AwardEntity ConvertDbObjectToEntity(Award dbObject)
	{
		return new AwardEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt, dbObject.Title,
			dbObject.Description, dbObject.VisualContentId);
	}
}

