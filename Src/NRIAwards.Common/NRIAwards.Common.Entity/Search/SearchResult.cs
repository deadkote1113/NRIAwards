namespace NRIAwards.Common.Entity.Search;

public class SearchResult<T>
{
    public SearchResult(int total, IEnumerable<T> objects, int requestedStartIndex, int? requestedObjectsCount)
    {
        Total = total;
        Objects = objects.ToList();
        RequestedObjectsCount = requestedObjectsCount;
        RequestedStartIndex = requestedStartIndex;
    }

    public SearchResult()
    {
    }

    public int Total { get; set; }
    public IList<T> Objects { get; set; }
    public int RequestedStartIndex { get; set; }
    public int? RequestedObjectsCount { get; set; }
}
