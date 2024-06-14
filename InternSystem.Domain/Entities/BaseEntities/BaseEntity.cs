namespace InternSystem.Domain.Entities.BaseEntities;

public class BaseEntity
{
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsDelete { get; set; } = false;
}