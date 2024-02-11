namespace NRIAwards.DAL.Context.Model;

public class Vote : PostgresModel
{
	public int NomineeId { get; set; }
	public bool IsCanceled { get; set; }
	public VoteTier Tier { get; set; }
	public string TelegramUsername { get; set; }
	public string? TelegramAvatar { get; set; }

	public virtual Nominee Nominee { get; set; }
}
