namespace NRIAwards.Common.Entity.Search;

public class AwardEventsSearchParams : BaseSearchParams
{
	public AwardEventsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
	{
	}

	public AwardEventsSearchParams() : base()
	{

	}

	public List<int> Ids { get; set; }
	public string TitleLike { get; set; }
	public string DescriptionLike { get; set; }
	public int? AwardsId { get; set; }
	public int? OrderId { get; set; }
	public int? VisualContentId { get; set; }
}
