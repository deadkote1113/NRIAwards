namespace NRIAwards.DAL.Context.Model;

public class Nominee : PostgresModel
{
	public string Title { get; set; }
	public string Description { get; set; }
	public int NominationId { get; set; }
	public int? VisualContentId { get; set; }

	public virtual Nomination Nomination { get; set; }
	public virtual VisualContent? VisualContent { get; }
	public virtual ICollection<Vote> Votes { get; } = new List<Vote>();
}
