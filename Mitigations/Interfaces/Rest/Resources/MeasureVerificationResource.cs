namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record MeasureVerificationResource(
    string Id, string TicketId, string? SupervisorId, string SupervisorName,
    string Verdict, string JustificationComment, DateTime VerificationDate,
    DateTimeOffset? CreatedAt, DateTimeOffset? UpdatedAt);
