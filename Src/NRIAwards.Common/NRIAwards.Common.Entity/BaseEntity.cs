using NRIAwards.Common.Heplers;

namespace NRIAwards.Common.Entity;

public class BaseEntity<TId>
{
    public BaseEntity(TId id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }

    public BaseEntity()
    {
        Id = default;
        CreatedAt = Helpers.GetCurrentDate();
        UpdatedAt = Helpers.GetCurrentDate();
        DeletedAt = null;
    }

    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
