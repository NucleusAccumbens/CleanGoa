using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class TlgUser : BaseAuditableEntity
{
    public long ChatId { get; set; }
    public string? Username { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsKicked { get; set; } = false;
    public bool CanNotify { get; set; } = true;
    public Language Language { get; set; }
}
