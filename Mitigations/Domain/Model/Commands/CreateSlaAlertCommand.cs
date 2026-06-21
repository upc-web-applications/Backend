namespace Acme.Center.Platform.Mitigations.Domain.Model.Commands;

public record CreateSlaAlertCommand(
    string TicketId,
    int ElapsedHours,
    int SlaLimitHours,
    DateTime AlertDate,
    string? NotifiedTo,
    string NotifiedName);
