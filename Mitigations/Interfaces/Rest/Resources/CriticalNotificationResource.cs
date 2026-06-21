namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record CriticalNotificationResource(
    string Id, string TicketId, string? SupervisorId, string SupervisorName,
    string Message, bool Sent, DateTime? SentDate,
    DateTimeOffset? CreatedAt, DateTimeOffset? UpdatedAt);
