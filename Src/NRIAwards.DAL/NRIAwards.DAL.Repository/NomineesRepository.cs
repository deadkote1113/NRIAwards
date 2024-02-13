using NomineeEntity = NRIAwards.Common.Entity.Nominee;

namespace NRIAwards.DAL.Repository;

public class NomineesRepository : BaseRepository<PostgresDbContext, Nominee, NomineeEntity, int, NomineesSearchParams, NomineesOrderParams, NomineesIncludeParams>,
	ICrudNomineesRepository, IExtendedNomineesRepository
{

	protected internal NomineesRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(NomineeEntity entity, Nominee dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.Title = entity.Title;
		dbObject.Description = entity.Description;
		dbObject.NominationId = entity.NominationId;
		dbObject.VisualContentId = entity.VisualContentId;
	}
	
	protected override IQueryable<Nominee> BuildDbQuery(IQueryable<Nominee> dbObjects, NomineesSearchParams searchParams)
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
			if(searchParams.NominationId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.NominationId == searchParams.NominationId);
			}
			if(searchParams.VisualContentId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.VisualContentId == searchParams.VisualContentId);
			}
        }
		return dbObjects;
	}

	protected override IQueryable<Nominee> OrderDbQuery(IQueryable<Nominee> dbObjects, NomineesOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<NomineeEntity>> BuildEntitiesListAsync(IQueryable<Nominee> dbObjects, NomineesIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static NomineeEntity ConvertDbObjectToEntity(Nominee dbObject)
	{
		return new NomineeEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt,
			dbObject.Title, dbObject.Description, dbObject.NominationId, dbObject.VisualContentId);
	}
}

