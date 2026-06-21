namespace Acme.Center.Platform.Mitigations.Domain.Model.Commands;

public record CreateMeasureVerificationCommand(
    string TicketId,
    string? SupervisorId,
    string SupervisorName,
    string Verdict,
    string JustificationComment,
    DateTime VerificationDate);
