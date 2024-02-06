using NRIAwards.Common.Entity.Enum;

namespace NRIAwards.Common.Entity;

public class User : BaseEntity<int>
{
    public User(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, string login,
        string password, UserRole roleId, bool isBlocked) : base(id, createdAt, updatedAt, deletedAt)
    {
        Login = login;
        Password = password;
        RoleId = roleId;
        IsBlocked = isBlocked;
    }

    public User() : base()
    {
    }

    public string Login { get; set; }
    public string Password { get; set; }
    public UserRole RoleId { get; set; }
    public bool IsBlocked { get; set; }
}
