namespace Acme.Center.Platform.Technicians.Interfaces.Rest.Resources;

public record CreateTechnicianResource(
    string DocumentNumber,
    string FullName,
    string Specialty,
    string Phone,
    string Email,
    string Status);
