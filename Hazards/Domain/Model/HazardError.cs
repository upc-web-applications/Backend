namespace Acme.Center.Platform.Hazards.Domain.Model;

public enum HazardError
{
    None,
    HazardNotFound,
    DuplicateHazardCode,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
