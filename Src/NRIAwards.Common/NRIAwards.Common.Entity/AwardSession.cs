namespace NRIAwards.Common.Entity;

public class AwardSession : BaseEntity<int>
{
	public AwardSession(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, int userId,
		string connectionCode, int state, int passedStagesCount, int awardId) : base(id, createdAt, updatedAt,
		deletedAt)
	{
		UserId = userId;
		ConnectionCode = connectionCode;
		State = state;
		PassedStagesCount = passedStagesCount;
		AwardId = awardId;
	}

	public AwardSession() : base()
	{
	}

	public int UserId { get; set; }
	public string ConnectionCode { get; set; }
	public int State { get; set; }
	public int PassedStagesCount { get; set; }
	public int AwardId { get; set; }
}

