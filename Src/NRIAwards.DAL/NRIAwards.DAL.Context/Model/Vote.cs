namespace NRIAwards.DAL.Context.Model;

public class Vote : PostgresModel
{
	public int NomineeId { get; set; }
	public bool IsCanseld { get; set; }
	public VoteTier Tier { get; set; }
	public string TelegramUserName { get; set; }
	public string? TelegramAvatar { get; set; }

	public virtual Nominee Nominee { get; set; }
}
