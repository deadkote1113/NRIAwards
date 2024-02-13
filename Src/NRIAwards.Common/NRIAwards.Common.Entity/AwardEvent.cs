namespace NRIAwards.Common.Entity;

public class AwardEvent : BaseEntity<int>
{
	public AwardEvent(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, string title,
		string description, int awardsId, int orderId, int visualContentId) : base(id, createdAt, updatedAt, deletedAt)
	{
		Title = title;
		Description = description;
		AwardsId = awardsId;
		OrderId = orderId;
		VisualContentId = visualContentId;
	}

	public AwardEvent() : base()
	{
	}

	public string Title { get; set; }
	public string Description { get; set; }
	public int AwardsId { get; set; }
	public int OrderId { get; set; }
	public int VisualContentId { get; set; }
}

