using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Technicians.Domain.Model.Aggregates;

public class Technician : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string DocumentNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
