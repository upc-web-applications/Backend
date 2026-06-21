namespace Acme.Center.Platform.Technicians.Domain.Model.Commands;

public record CreateTechnicianCommand(
    string DocumentNumber,
    string FullName,
    string Specialty,
    string Phone,
    string Email,
    string Status);
