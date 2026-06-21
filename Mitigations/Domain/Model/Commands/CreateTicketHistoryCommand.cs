namespace Acme.Center.Platform.Mitigations.Domain.Model.Commands;

public record CreateTicketHistoryCommand(
    string TicketId,
    string Event,
    string? UserId,
    string UserName,
    string Details,
    DateTime Date);
