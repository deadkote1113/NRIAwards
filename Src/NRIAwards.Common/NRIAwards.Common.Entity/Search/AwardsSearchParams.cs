namespace NRIAwards.Common.Entity.Search;

public class AwardsSearchParams : BaseSearchParams
{
	public AwardsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
	{
	}

	public AwardsSearchParams() : base()
	{

	}

	public List<int> Ids { get; set; }
	public string TitleLike { get; set; }
	public string DescriptionLike { get; set; }
	public int? VisualContentId { get; set; }
}
