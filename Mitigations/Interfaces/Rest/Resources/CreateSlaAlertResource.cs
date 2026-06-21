namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record CreateSlaAlertResource(
    string TicketId, int ElapsedHours, int SlaLimitHours,
    DateTime AlertDate, string? NotifiedTo, string NotifiedName);
