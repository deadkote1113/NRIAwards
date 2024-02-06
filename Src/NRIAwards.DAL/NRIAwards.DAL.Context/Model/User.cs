using NRIAwards.Common.Entity.Enum;

namespace NRIAwards.DAL.Context.Model;

public class User : PostgresModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    public UserRole RoleId { get; set; }
    public bool IsBlocked { get; set; }
}
