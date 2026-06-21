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

public class RiskPatternCommandService(
    IRiskPatternRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : IRiskPatternCommandService
{
    public async Task<Result<RiskPattern>> Handle(CreateRiskPatternCommand command, CancellationToken cancellationToken)
    {
        var entity = new RiskPattern
        {
            SectorId = command.SectorId,
            Sector = command.Sector,
            IncidentType = command.IncidentType,
            HazardType = command.HazardType,
            Description = command.Description,
            Frequency = command.Frequency,
            FirstOccurrenceDate = command.FirstOccurrenceDate,
            AnalysisPeriodDays = command.AnalysisPeriodDays,
            IsReviewed = command.IsReviewed,
            ReviewDate = command.ReviewDate,
            ReviewedBy = command.ReviewedBy
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<RiskPattern>.Success(entity);
        }
        catch (OperationCanceledException) { return Result<RiskPattern>.Failure(RiskPatternError.OperationCancelled, localizer[nameof(RiskPatternError.OperationCancelled)]); }
        catch (DbUpdateException) { return Result<RiskPattern>.Failure(RiskPatternError.DatabaseError, localizer[nameof(RiskPatternError.DatabaseError)]); }
        catch (Exception) { return Result<RiskPattern>.Failure(RiskPatternError.InternalServerError, localizer[nameof(RiskPatternError.InternalServerError)]); }
    }
}
