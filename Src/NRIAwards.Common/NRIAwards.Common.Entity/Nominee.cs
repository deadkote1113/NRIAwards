namespace NRIAwards.Common.Entity;

public class Nominee : BaseEntity<int>
{
	public Nominee(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, string title,
		string description, int nominationId, int visualContentId) : base(id, createdAt, updatedAt, deletedAt)
	{
		Title = title;
		Description = description;
		NominationId = nominationId;
		VisualContentId = visualContentId;
	}

	public Nominee() : base()
	{
	}

	public string Title { get; set; }
	public string Description { get; set; }
	public int NominationId { get; set; }
	public int VisualContentId { get; set; }
}

