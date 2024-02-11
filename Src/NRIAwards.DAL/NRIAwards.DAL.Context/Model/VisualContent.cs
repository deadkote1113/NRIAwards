namespace NRIAwards.DAL.Context.Model;

public class VisualContent : PostgresModel
{
	public VisualContentType Type { get; set; }
	public string? Title { get; set; }
	public string Link { get; set; }

	public virtual ICollection<Award> Awards { get; } = new List<Award>();
	public virtual ICollection<Nomination> Nominations { get; } = new List<Nomination>();
	public virtual ICollection<Nominee> Nominees { get; } = new List<Nominee>();
	public virtual ICollection<User> Users { get; } = new List<User>();
}
