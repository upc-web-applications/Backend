namespace Acme.Center.Platform.Iam.Domain.Model.Aggregates;

public class Session
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string UserId { get; set; } = string.Empty;
    public string TokenSignature { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastActivityAt { get; set; } = DateTime.UtcNow;
    public bool IsValid { get; set; } = true;
    public DateTime? ClosedAt { get; set; }
    public string CloseReason { get; set; } = string.Empty;
}
