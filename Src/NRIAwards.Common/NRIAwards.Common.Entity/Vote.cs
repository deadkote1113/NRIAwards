namespace NRIAwards.Common.Entity;

public class Vote : BaseEntity<int>
{
	public Vote(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, int nomineeId, bool isCanceled,
		NRIAwards.Common.Entity.Enum.VoteTier tier, string telegramUsername, string telegramAvatar) : base(id,
		createdAt, updatedAt, deletedAt)
	{
		NomineeId = nomineeId;
		IsCanceled = isCanceled;
		Tier = tier;
		TelegramUsername = telegramUsername;
		TelegramAvatar = telegramAvatar;
	}

	public Vote() : base()
	{
	}

	public int NomineeId { get; set; }
	public bool IsCanceled { get; set; }
	public NRIAwards.Common.Entity.Enum.VoteTier Tier { get; set; }
	public string TelegramUsername { get; set; }
	public string TelegramAvatar { get; set; }
}

