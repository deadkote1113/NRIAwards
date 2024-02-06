namespace NRIAwards.BL.Service;

public class UsersService : BaseService<User, int, UsersSearchParams, UsersOrderParams, UsersIncludeParams>,
    ICrudUsersService, IExtendedUsersService
{
    private readonly IExtendedUsersRepository _extendedUsersRepository;

    public UsersService(ICrudUsersRepository repository,
        IExtendedUsersRepository extendedUsersRepository) : base(repository)
    {
        _extendedUsersRepository = extendedUsersRepository;
    }

    public async Task<User> VerifyPasswordAsync(string login, string password)
    {
        var user = await GetFirstAsync(new UsersSearchParams()
        {
            Login = login,
        });
        var passwordHash = Helpers.GetPasswordHash(password);
        return user != null && user.Password == passwordHash ? user : null;
    }
}
