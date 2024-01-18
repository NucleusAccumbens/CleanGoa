using Domain.Common;

namespace Domain.Entities;

public class Event : BaseAuditableEntity
{ 
    public DateTime Date { get; set; }

    public string Description { get; set; }
}
