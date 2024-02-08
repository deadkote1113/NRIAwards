using System.ComponentModel.DataAnnotations;

namespace NRIAwards.PL.UI.Models;

public class UserModel : BaseModel<int>
{
    [Required(ErrorMessage = "Укажите значение")]
    [Display(Name = "Заблокирован")]
    public bool IsBlocked { get; set; }

    [Required(ErrorMessage = "Укажите значение")]
    [Display(Name = "Логин")]
    public string Login { get; set; }

    [Display(Name = "Пароль")]
    public string? Password { get; set; }

    [Display(Name = "Дата регистрации")]
    public DateTime RegistrationDate { get; set; }

    [Required(ErrorMessage = "Укажите значение")]
    [Display(Name = "Роль")]
    public UserRole Role { get; set; }

    public static UserModel? FromEntity(User? entity)
    {
        if (entity == null)
        {
            return null;
        }
        return new UserModel
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            IsBlocked = entity.IsBlocked,
            Login = entity.Login,
            Password = entity.Password,
            Role = entity.RoleId,
        };
    }

    public static User? ToEntity(UserModel? model)
    {
        if (model == null)
        {
            return null;
        }
        return new User(model.Id, model.CreatedAt, model.UpdatedAt, model.DeletedAt, model.Login, model.Password, model.Role, model.IsBlocked);
    }

    public static List<UserModel?> FromEntitiesList(IEnumerable<User?> list)
    {
        return list.Select(FromEntity).ToList();
    }

    public static List<User?> ToEntitiesList(IEnumerable<UserModel?> list)
    {
        return list.Select(ToEntity).ToList();
    }
}