namespace Acme.Center.Platform.Mitigations.Domain.Model;

public enum MitigationError
{
    None,
    MitigationNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
