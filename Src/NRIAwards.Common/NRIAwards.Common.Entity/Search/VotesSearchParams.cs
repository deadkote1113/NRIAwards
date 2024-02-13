namespace NRIAwards.Common.Entity.Search;

public class VotesSearchParams : BaseSearchParams
{
	public VotesSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
	{
	}

	public VotesSearchParams() : base()
	{

	}

	public List<int> Ids { get; set; }
	public int? NomineeId { get; set; }
	public bool? IsIsCanceled { get; set; }
	public string TelegramUsernameLike { get; set; }
	public string TelegramAvatarLike { get; set; }
}
