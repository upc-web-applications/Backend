namespace Acme.Center.Platform.Mitigations.Domain.Model;

public enum TicketHistoryError
{
    None,
    TicketHistoryNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
