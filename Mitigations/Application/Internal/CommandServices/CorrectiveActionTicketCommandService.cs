using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Acme.Center.Platform.Mitigations.Application.CommandServices;
using Acme.Center.Platform.Mitigations.Domain.Model;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Resources.Errors;

namespace Acme.Center.Platform.Mitigations.Application.Internal.CommandServices;

public class CorrectiveActionTicketCommandService(ICorrectiveActionTicketRepository repository, IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessage> localizer) : ICorrectiveActionTicketCommandService
{
    public async Task<Result<CorrectiveActionTicket>> Handle(CreateCorrectiveActionTicketCommand command, CancellationToken cancellationToken)
    {
        var entity = new CorrectiveActionTicket
        {
            TicketNumber = command.TicketNumber,
            ReportId = command.ReportId,
            SectorId = command.SectorId,
            Sector = command.Sector,
            RiskType = command.RiskType,
            CriticalityLevel = command.CriticalityLevel,
            Status = command.Status,
            Instructions = command.Instructions,
            AssignedTechnicianId = command.AssignedTechnicianId,
            TechnicianName = command.TechnicianName,
            CreatedDate = command.CreatedDate,
            ClosureDate = command.ClosureDate,
            SlaLimitHours = command.SlaLimitHours,
            SlaMissed = command.SlaMissed
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<CorrectiveActionTicket>.Success(entity);
        }
        catch (OperationCanceledException)
        {
            return Result<CorrectiveActionTicket>.Failure(CorrectiveActionTicketError.OperationCancelled, localizer[nameof(CorrectiveActionTicketError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<CorrectiveActionTicket>.Failure(CorrectiveActionTicketError.DatabaseError, localizer[nameof(CorrectiveActionTicketError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<CorrectiveActionTicket>.Failure(CorrectiveActionTicketError.InternalServerError, localizer[nameof(CorrectiveActionTicketError.InternalServerError)]);
        }
    }
}
