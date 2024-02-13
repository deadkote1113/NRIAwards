namespace NRIAwards.Common.Entity.Search;

public class ReadersSearchParams : BaseSearchParams
{
	public ReadersSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
	{
	}

	public ReadersSearchParams() : base()
	{

	}

	public List<int> Ids { get; set; }
	public string NameLike { get; set; }
}
