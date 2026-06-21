using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Acme.Center.Platform.RiskAssessments.Application.CommandServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Resources.Errors;

namespace Acme.Center.Platform.RiskAssessments.Application.Internal.CommandServices;

public class DailySummaryCommandService(
    IDailySummaryRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : IDailySummaryCommandService
{
    public async Task<Result<DailySummary>> Handle(CreateDailySummaryCommand command, CancellationToken cancellationToken)
    {
        var entity = new DailySummary
        {
            Date = command.Date,
            SectorId = command.SectorId,
            Sector = command.Sector,
            TotalNew = command.TotalNew,
            TotalInProgress = command.TotalInProgress,
            TotalResolved = command.TotalResolved
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<DailySummary>.Success(entity);
        }
        catch (OperationCanceledException) { return Result<DailySummary>.Failure(DailySummaryError.OperationCancelled, localizer[nameof(DailySummaryError.OperationCancelled)]); }
        catch (DbUpdateException) { return Result<DailySummary>.Failure(DailySummaryError.DatabaseError, localizer[nameof(DailySummaryError.DatabaseError)]); }
        catch (Exception) { return Result<DailySummary>.Failure(DailySummaryError.InternalServerError, localizer[nameof(DailySummaryError.InternalServerError)]); }
    }
}
