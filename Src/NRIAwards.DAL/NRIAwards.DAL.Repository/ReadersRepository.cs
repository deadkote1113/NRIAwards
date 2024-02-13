using ReaderEntity = NRIAwards.Common.Entity.Reader;

namespace NRIAwards.DAL.Repository;

public class ReadersRepository : BaseRepository<PostgresDbContext, Reader, ReaderEntity, int, ReadersSearchParams, ReadersOrderParams, ReadersIncludeParams>,
	ICrudReadersRepository, IExtendedReadersRepository
{

	protected internal ReadersRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(ReaderEntity entity, Reader dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.Name = entity.Name;
	}
	
	protected override IQueryable<Reader> BuildDbQuery(IQueryable<Reader> dbObjects, ReadersSearchParams searchParams)
	{
		if (searchParams != null)
		{
			dbObjects = base.BuildDbQuery(dbObjects, searchParams);
			if (searchParams.Ids != null)
			{
				dbObjects = dbObjects.Where(item => searchParams.Ids.Contains(item.Id));
			}
			if(string.IsNullOrEmpty(searchParams.NameLike) == false)
			{
				dbObjects = dbObjects.Where(item => item.Name.ToLower().Contains(searchParams.NameLike.ToLower()));
			}
        }
		return dbObjects;
	}

	protected override IQueryable<Reader> OrderDbQuery(IQueryable<Reader> dbObjects, ReadersOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<ReaderEntity>> BuildEntitiesListAsync(IQueryable<Reader> dbObjects, ReadersIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static ReaderEntity ConvertDbObjectToEntity(Reader dbObject)
	{
		return new ReaderEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt, dbObject.Name);
	}
}

