namespace Acme.Center.Platform.Mitigations.Domain.Model;

public enum CorrectiveActionTicketError
{
    None,
    CorrectiveActionTicketNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
