using NominationEntity = NRIAwards.Common.Entity.Nomination;

namespace NRIAwards.DAL.Repository;

public class NominationsRepository : BaseRepository<PostgresDbContext, Nomination, NominationEntity, int, NominationsSearchParams, NominationsOrderParams, NominationsIncludeParams>,
	ICrudNominationsRepository, IExtendedNominationsRepository
{

	protected internal NominationsRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(NominationEntity entity, Nomination dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.Title = entity.Title;
		dbObject.Description = entity.Description;
		dbObject.OrderId = entity.OrderId;
		dbObject.AwardsId = entity.AwardsId;
		dbObject.ReaderId = entity.ReaderId;
		dbObject.VisualContentId = entity.VisualContentId;
	}
	
	protected override IQueryable<Nomination> BuildDbQuery(IQueryable<Nomination> dbObjects, NominationsSearchParams searchParams)
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
			if(searchParams.OrderId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.OrderId == searchParams.OrderId);
			}
			if(searchParams.AwardsId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.AwardsId == searchParams.AwardsId);
			}
			if(searchParams.ReaderId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.ReaderId == searchParams.ReaderId);
			}
			if(searchParams.VisualContentId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.VisualContentId == searchParams.VisualContentId);
			}
        }
		return dbObjects;
	}

	protected override IQueryable<Nomination> OrderDbQuery(IQueryable<Nomination> dbObjects, NominationsOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<NominationEntity>> BuildEntitiesListAsync(IQueryable<Nomination> dbObjects, NominationsIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static NominationEntity ConvertDbObjectToEntity(Nomination dbObject)
	{
		return new NominationEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt,
			dbObject.Title, dbObject.Description, dbObject.OrderId, dbObject.AwardsId, dbObject.ReaderId,
			dbObject.VisualContentId);
	}
}

