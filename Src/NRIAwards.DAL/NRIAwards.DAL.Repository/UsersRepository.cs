using UserEntity = NRIAwards.Common.Entity.User;

namespace NRIAwards.DAL.Repository;

public class UsersRepository : BaseRepository<PostgresDbContext, User, UserEntity, int, UsersSearchParams, UsersOrderParams, UsersIncludeParams>,
    ICrudUsersRepository, IExtendedUsersRepository
{
    public UsersRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override bool RequiresUpdatesAfterObjectSaving => false;

    protected override async Task UpdateBeforeSavingAsync(UserEntity entity, User dbObject, bool exists)
    {
        await base.UpdateBeforeSavingAsync(entity, dbObject, exists);

        dbObject.IsBlocked = entity.IsBlocked;
        dbObject.Login = entity.Login;
        if (exists == false || entity.Password != null)
        {
            dbObject.Password = entity.Password;
        }
        dbObject.RoleId = entity.RoleId;
    }

    protected override IQueryable<User> BuildDbQuery(IQueryable<User> dbObjects, UsersSearchParams searchParams)
    {
        dbObjects = base.BuildDbQuery(dbObjects, searchParams);

        if (searchParams != null)
        {
            if (searchParams.Login is not null)
            {
                dbObjects = dbObjects.Where(item => item.Login == searchParams.Login);
            }
            if (searchParams.Ids is not null)
            {
                dbObjects = dbObjects.Where(item => searchParams.Ids.Contains(item.Id));
            }
            if (searchParams.Roles is not null)
            {
                dbObjects = dbObjects.Where(item => searchParams.Roles.Contains(item.RoleId));
            }
            if (searchParams.SearchQuery is not null && string.IsNullOrEmpty(searchParams.SearchQuery) == false)
            {
                dbObjects = dbObjects.Where(item => item.Login.Trim().ToLower() == searchParams.SearchQuery.Trim().ToLower());
            }
        }

        return dbObjects;
    }

    protected override IQueryable<User> OrderDbQuery(IQueryable<User> dbObjects, UsersOrderParams orderParams)
    {
        dbObjects = base.OrderDbQuery(dbObjects, orderParams);

        if (orderParams != null)
        {

        }

        return dbObjects;
    }

    protected override async Task<IList<UserEntity>> BuildEntitiesListAsync(IQueryable<User> dbObjects, UsersIncludeParams? includeParams)
    {
        return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
    }

    internal static UserEntity ConvertDbObjectToEntity(User dbObject)
    {
        return new UserEntity(dbObject.Id, dbObject.CreatedAt, dbObject.UpdatedAt, dbObject.DeletedAt,
            dbObject.Login, dbObject.Password, dbObject.RoleId, dbObject.IsBlocked);
    }
}
