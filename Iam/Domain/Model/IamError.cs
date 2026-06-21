namespace Acme.Center.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    UserNotFound,
    InvalidCredentials,
    UsernameAlreadyTaken,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
