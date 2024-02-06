namespace NRIAwards.Common.Entity.Search;

public class BaseSearchParams
{
    public BaseSearchParams(int startIndex = 0, int? objectsCount = null)
    {
        StartIndex = startIndex;
        ObjectsCount = objectsCount;
        ExcludeDeleted = true;
    }

    public int StartIndex { get; set; }
    public int? ObjectsCount { get; set; }
    public bool ExcludeDeleted { get; set; }
}
