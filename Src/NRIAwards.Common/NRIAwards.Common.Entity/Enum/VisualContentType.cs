using System.ComponentModel.DataAnnotations;

namespace NRIAwards.Common.Entity.Enum;

public enum VisualContentType
{
	[Display(Name = "Фотография")]
	Picture = 1,
	[Display(Name = "Видео с ютуба")]
	YoutubeVideo = 2
}
