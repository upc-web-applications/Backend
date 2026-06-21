namespace Acme.Center.Platform.Mitigations.Domain.Model;

public enum SlaAlertError
{
    None,
    SlaAlertNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
