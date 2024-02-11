using System.ComponentModel.DataAnnotations;

namespace NRIAwards.Common.Entity.Enum;

public enum VoteTier
{
	[Display(Name = "Золото")]
	Gold = 1,
	[Display(Name = "Серебро")]
	Silver = 2,
	[Display(Name = "Бронза")]
	Bronze = 3,
}
