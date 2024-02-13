namespace NRIAwards.Common.Entity;

public class VisualContent : BaseEntity<int>
{
	public VisualContent(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt,
		NRIAwards.Common.Entity.Enum.VisualContentType type, string title, string link) : base(id, createdAt,
		updatedAt, deletedAt)
	{
		Type = type;
		Title = title;
		Link = link;
	}

	public VisualContent() : base()
	{
	}

	public NRIAwards.Common.Entity.Enum.VisualContentType Type { get; set; }
	public string Title { get; set; }
	public string Link { get; set; }
}

