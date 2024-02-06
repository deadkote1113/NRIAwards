namespace NRIAwards.BL.Base.Interface.Extended;

public interface IExtendedUsersService
{
    Task<User> VerifyPasswordAsync(string login, string password);
}
