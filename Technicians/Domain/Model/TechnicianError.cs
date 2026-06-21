namespace Acme.Center.Platform.Technicians.Domain.Model;

public enum TechnicianError
{
    None,
    TechnicianNotFound,
    DuplicateDocumentNumber,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
