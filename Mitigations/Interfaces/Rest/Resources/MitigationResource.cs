namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record MitigationResource(
    string Id, string? RiskAssessmentId, string? TicketId, string Code, string Description,
    string Responsible, DateTime AssignedDate, DateTime? ExecutionDate,
    string Status, string? Result, string? Observations,
    DateTimeOffset? CreatedAt, DateTimeOffset? UpdatedAt);
