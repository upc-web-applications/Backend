namespace Acme.Center.Platform.Technicians.Interfaces.Rest.Resources;

public record TechnicianResource(
    string Id,
    string DocumentNumber,
    string FullName,
    string Specialty,
    string Phone,
    string Email,
    string Status,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
