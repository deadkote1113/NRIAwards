namespace NRIAwards.DAL.Context.Model;

public class Nomination : PostgresModel
{
	public string Title { get; set; }
	public string Description { get; set; }
	public int OrderId { get; set; }
	public int AwardsId { get; set; }
	public int? ReaderId { get; set; }
	public int VisualContentId { get; set; }

	public virtual Award Award { get; set; }
	public virtual Reader? Reader { get; set; }
	public virtual VisualContent VisualContent { get; }
	public virtual ICollection<Nominee> Nominees { get; } = new List<Nominee>();
}
