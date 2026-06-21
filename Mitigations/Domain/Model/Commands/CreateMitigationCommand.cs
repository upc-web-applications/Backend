namespace Acme.Center.Platform.Mitigations.Domain.Model.Commands;

public record CreateMitigationCommand(
    string? RiskAssessmentId,
    string? TicketId,
    string Code,
    string Description,
    string Responsible,
    DateTime AssignedDate,
    DateTime? ExecutionDate,
    string Status,
    string? Result,
    string? Observations);
