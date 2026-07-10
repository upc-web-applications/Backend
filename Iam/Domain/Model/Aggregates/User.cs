using System.Text.Json.Serialization;
using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.Iam.Domain.Model.Aggregates;

public class User : IAuditableEntity
{
    public User()
    {
        Id = Guid.NewGuid().ToString("N");
        Username = string.Empty;
        PasswordHash = string.Empty;
    }

    public User(string username, string passwordHash) : this()
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public User(string username, string passwordHash, string email, string name) : this()
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        Name = name;
    }

    public string Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "Operator";
    public string RoleId { get; set; } = string.Empty;
    public int? SectorId { get; set; }
    public string AccountStatus { get; set; } = "ACTIVE";

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public int FailedAttempts { get; set; }
    public DateTime? LockedUntil { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
