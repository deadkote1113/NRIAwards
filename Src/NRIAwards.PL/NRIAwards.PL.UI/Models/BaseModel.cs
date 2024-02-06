using System.ComponentModel.DataAnnotations;

namespace NRIAwards.PL.Ui.Models;

public class BaseModel<TId>
{
    [Display(Name = "Идентификатор")]
    public TId Id { get; set; }

    [Display(Name = "Дата создания")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Дата последнего обновления")]
    public DateTime UpdatedAt { get; set; }

    [Display(Name = "Дата удаления")]
    public DateTime? DeletedAt { get; set; }
}
