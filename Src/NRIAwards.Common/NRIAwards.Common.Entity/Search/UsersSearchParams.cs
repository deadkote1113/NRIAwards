using NRIAwards.Common.Entity.Enum;

namespace NRIAwards.Common.Entity.Search;

public class UsersSearchParams : BaseSearchParams
{
    public UsersSearchParams(int startIndex = 0, int? objectsCount = null) : base(startIndex, objectsCount)
    {
    }

    public UsersSearchParams() : base()
    {

    }

    public List<int> Ids { get; set; }
    public string? Login { get; set; }
    public IEnumerable<UserRole>? Roles { get; set; }
    public string? SearchQuery { get; set; }

}
