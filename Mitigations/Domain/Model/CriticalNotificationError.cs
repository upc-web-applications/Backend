namespace Acme.Center.Platform.Mitigations.Domain.Model;

public enum CriticalNotificationError
{
    None,
    CriticalNotificationNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
