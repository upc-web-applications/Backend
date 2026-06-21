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

    public string Id { get; set; }
    public string Username { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
