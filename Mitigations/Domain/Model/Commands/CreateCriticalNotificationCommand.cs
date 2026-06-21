namespace Acme.Center.Platform.Mitigations.Domain.Model.Commands;

public record CreateCriticalNotificationCommand(
    string TicketId,
    string? SupervisorId,
    string SupervisorName,
    string Message,
    bool Sent,
    DateTime? SentDate);
