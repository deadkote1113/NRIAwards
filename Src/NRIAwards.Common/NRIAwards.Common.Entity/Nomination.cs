namespace NRIAwards.Common.Entity;

public class Nomination : BaseEntity<int>
{
	public Nomination(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, string title,
		string description, int orderId, int awardsId, int? readerId, int visualContentId) : base(id, createdAt,
		updatedAt, deletedAt)
	{
		Title = title;
		Description = description;
		OrderId = orderId;
		AwardsId = awardsId;
		ReaderId = readerId;
		VisualContentId = visualContentId;
	}

	public Nomination() : base()
	{
	}

	public string Title { get; set; }
	public string Description { get; set; }
	public int OrderId { get; set; }
	public int AwardsId { get; set; }
	public int? ReaderId { get; set; }
	public int VisualContentId { get; set; }
}

