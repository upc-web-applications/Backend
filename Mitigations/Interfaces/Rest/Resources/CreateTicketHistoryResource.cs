namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record CreateTicketHistoryResource(
    string TicketId, string Event, string? UserId,
    string UserName, string Details, DateTime Date);
