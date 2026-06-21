namespace Acme.Center.Platform.Iam.Domain.Model.Aggregates;

public class AccessLog
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string? UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime AttemptAt { get; set; } = DateTime.UtcNow;
    public bool WasSuccessful { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string FailureReason { get; set; } = string.Empty;
}
