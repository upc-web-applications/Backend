namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record SlaAlertResource(
    string Id, string TicketId, int ElapsedHours, int SlaLimitHours,
    DateTime AlertDate, string? NotifiedTo, string NotifiedName,
    DateTimeOffset? CreatedAt, DateTimeOffset? UpdatedAt);
