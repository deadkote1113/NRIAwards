namespace NRIAwards.Common.Entity.Search;

public class VisualContentsSearchParams : BaseSearchParams
{
	public VisualContentsSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
	{
	}

	public VisualContentsSearchParams() : base()
	{

	}

	public List<int> Ids { get; set; }
	public string TitleLike { get; set; }
	public string LinkLike { get; set; }
}
