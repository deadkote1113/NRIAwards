using System.ComponentModel.DataAnnotations;

namespace NRIAwards.PL.UI.Models.ViewModels.FilterModels;

public class UsersFilterModel : BaseFilterModel
{
    [Display(Name = "Поисковый запрос")]
    public string SearchQuery { get; set; }
}