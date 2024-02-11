namespace NRIAwards.DAL.Context.Model;

public class Award : PostgresModel
{
	public string Title { get; set; }
	public string Description { get; set; }
	public int? VisualContentId { get; set; }

	public virtual VisualContent? VisualContent { get; }
	public virtual ICollection<AwardSession> AwardSessions { get; } = new List<AwardSession>();
    public virtual ICollection<Nomination> Nominations { get; } = new List<Nomination>();
	public virtual ICollection<AwardEvent> AwardEvents { get; } = new List<AwardEvent>();
}
