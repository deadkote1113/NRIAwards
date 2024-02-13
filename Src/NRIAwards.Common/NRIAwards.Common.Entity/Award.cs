namespace NRIAwards.Common.Entity;

public class Award : BaseEntity<int>
{
	public Award(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, string title, string description,
		int visualContentId) : base(id, createdAt, updatedAt, deletedAt)
	{
		Title = title;
		Description = description;
		VisualContentId = visualContentId;
	}

	public Award() : base()
	{
	}

	public string Title { get; set; }
	public string Description { get; set; }
	public int VisualContentId { get; set; }
}

