namespace Acme.Center.Platform.Mitigations.Domain.Model;

public enum MeasureVerificationError
{
    None,
    MeasureVerificationNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
