namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record CreateCriticalNotificationResource(
    string TicketId, string? SupervisorId, string SupervisorName,
    string Message, bool Sent, DateTime? SentDate);
