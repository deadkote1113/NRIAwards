namespace NRIAwards.DAL.Context.Model;

public class Reader : PostgresModel
{
	public string Name { get; set; }

	public virtual ICollection<Nomination> Nominations { get; set; } = new List<Nomination>();
}
