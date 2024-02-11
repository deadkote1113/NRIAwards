namespace NRIAwards.DAL.Context.Model;

public class AwardSession : PostgresModel
{
	public int UserId { get; set; }
	public string ConnectionCode { get; set; }
	public int State { get; set; }
	public int PassedStagesCount { get; set; }
	public int AwardId { get; set; }

	public virtual Award Award { get; set; }
	public virtual User User { get; set; }
}
