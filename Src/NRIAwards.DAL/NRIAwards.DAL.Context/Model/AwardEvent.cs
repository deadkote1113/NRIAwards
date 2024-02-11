namespace NRIAwards.DAL.Context.Model;

public class AwardEvent : PostgresModel
{
	public string Title { get; set; }
	public string Description { get; set; }
	public int AwardsId { get; set; }
	public int OrderId { get; set; }
	public int VisualContentId { get; set; }

	public virtual Award Award { get; set; }
	public virtual VisualContent VisualContent { get; }
}
