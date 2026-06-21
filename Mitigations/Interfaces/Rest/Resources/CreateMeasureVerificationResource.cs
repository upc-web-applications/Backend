namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record CreateMeasureVerificationResource(
    string TicketId, string? SupervisorId, string SupervisorName,
    string Verdict, string JustificationComment, DateTime VerificationDate);
