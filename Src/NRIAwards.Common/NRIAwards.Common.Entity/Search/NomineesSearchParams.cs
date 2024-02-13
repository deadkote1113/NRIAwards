namespace NRIAwards.Common.Entity.Search;

public class NomineesSearchParams : BaseSearchParams
{
	public NomineesSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
	{
	}

	public NomineesSearchParams() : base()
	{

	}

	public List<int> Ids { get; set; }
	public string TitleLike { get; set; }
	public string DescriptionLike { get; set; }
	public int? NominationId { get; set; }
	public int? VisualContentId { get; set; }
}
