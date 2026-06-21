namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record TicketHistoryResource(
    string Id, string TicketId, string Event, string? UserId,
    string UserName, string Details, DateTime Date,
    DateTimeOffset? CreatedAt, DateTimeOffset? UpdatedAt);
