namespace NRIAwards.Common.Entity.Search;

public class AwardSessionsSearchParams : BaseSearchParams
{
	public AwardSessionsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
	{
	}

	public AwardSessionsSearchParams() : base()
	{

	}

	public List<int> Ids { get; set; }
	public int? UserId { get; set; }
	public string ConnectionCodeLike { get; set; }
	public int? State { get; set; }
	public int? PassedStagesCount { get; set; }
	public int? AwardId { get; set; }
}
