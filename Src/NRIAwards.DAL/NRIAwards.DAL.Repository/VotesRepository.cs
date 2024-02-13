using VoteEntity = NRIAwards.Common.Entity.Vote;

namespace NRIAwards.DAL.Repository;

public class VotesRepository : BaseRepository<PostgresDbContext, Vote, VoteEntity, int, VotesSearchParams, VotesOrderParams, VotesIncludeParams>,
	ICrudVotesRepository, IExtendedVotesRepository
{

	protected internal VotesRepository(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override bool RequiresUpdatesAfterObjectSaving => false;

	protected override async Task UpdateBeforeSavingAsync(VoteEntity entity, Vote dbObject, bool exists)
	{
		await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

		dbObject.NomineeId = entity.NomineeId;
		dbObject.IsCanceled = entity.IsCanceled;
		dbObject.Tier = entity.Tier;
		dbObject.TelegramUsername = entity.TelegramUsername;
		dbObject.TelegramAvatar = entity.TelegramAvatar;
	}
	
	protected override IQueryable<Vote> BuildDbQuery(IQueryable<Vote> dbObjects, VotesSearchParams searchParams)
	{
		if (searchParams != null)
		{
			dbObjects = base.BuildDbQuery(dbObjects, searchParams);
			if (searchParams.Ids != null)
			{
				dbObjects = dbObjects.Where(item => searchParams.Ids.Contains(item.Id));
			}
			if(searchParams.NomineeId.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.NomineeId == searchParams.NomineeId);
			}
			if(searchParams.IsIsCanceled.HasValue)
			{
				dbObjects = dbObjects.Where(item => item.IsCanceled == searchParams.IsIsCanceled);

			}
			if(string.IsNullOrEmpty(searchParams.TelegramUsernameLike) == false)
			{
				dbObjects = dbObjects.Where(item => item.TelegramUsername.ToLower().Contains(searchParams.TelegramUsernameLike.ToLower()));
			}
			if(string.IsNullOrEmpty(searchParams.TelegramAvatarLike) == false)
			{
				dbObjects = dbObjects.Where(item => item.TelegramAvatar.ToLower().Contains(searchParams.TelegramAvatarLike.ToLower()));
			}
        }
		return dbObjects;
	}

	protected override IQueryable<Vote> OrderDbQuery(IQueryable<Vote> dbObjects, VotesOrderParams orderParams)
	{
		dbObjects = base.OrderDbQuery(dbObjects, orderParams);

		if (orderParams != null)
		{

		}

		return dbObjects;
	}

	protected override async Task<IList<VoteEntity>> BuildEntitiesListAsync(IQueryable<Vote> dbObjects, VotesIncludeParams includeParams)
	{
		return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
	}

	internal static VoteEntity ConvertDbObjectToEntity(Vote dbObject)
	{
		return new VoteEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt,
			dbObject.NomineeId, dbObject.IsCanceled, dbObject.Tier, dbObject.TelegramUsername, dbObject.TelegramAvatar);
	}
}

