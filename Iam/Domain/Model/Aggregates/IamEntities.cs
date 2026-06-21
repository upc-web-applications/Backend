using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RiskGuard.Platform.Iam.Domain.Model.Aggregates;

public class Role
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "Operario";
    public string RoleId { get; set; } = string.Empty;
    public int? SectorId { get; set; }

    [JsonPropertyName("account_status")]
    public string AccountStatus { get; set; } = "ACTIVE";

    [JsonPropertyName("creation_date")]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("site_area_id")]
    public string? SiteAreaId { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public int FailedAttempts { get; set; }
    public DateTime? LockedUntil { get; set; }

    [NotMapped]
    public string FullName
    {
        get => Name;
        set => Name = value;
    }

    [NotMapped]
    public string Password { get; set; } = "Risk123";

    [NotMapped]
    public string Status
    {
        get => AccountStatus.ToLowerInvariant();
        set => AccountStatus = value.ToUpperInvariant();
    }

    [NotMapped]
    public DateTime? BlockedUntil
    {
        get => LockedUntil;
        set => LockedUntil = value;
    }
}

public class Session
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string UserId { get; set; } = string.Empty;
    public string TokenSignature { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastActivityDate { get; set; } = DateTime.UtcNow;
    public bool IsValid { get; set; } = true;
    public DateTime? ClosedAt { get; set; }
    public string CloseReason { get; set; } = string.Empty;

    [NotMapped]
    public string Token
    {
        get => TokenSignature;
        set => TokenSignature = value;
    }

    [NotMapped]
    public DateTime CreatedAt
    {
        get => CreationDate;
        set => CreationDate = value;
    }

    [NotMapped]
    public DateTime LastActivityAt
    {
        get => LastActivityDate;
        set => LastActivityDate = value;
    }
}

public class AccessLog
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string? UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime AttemptDate { get; set; } = DateTime.UtcNow;
    public bool WasSuccessful { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string FailureReason { get; set; } = string.Empty;

    [NotMapped]
    public DateTime AttemptAt
    {
        get => AttemptDate;
        set => AttemptDate = value;
    }
}
