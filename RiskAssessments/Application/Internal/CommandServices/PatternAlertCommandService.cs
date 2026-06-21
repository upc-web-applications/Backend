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

public class PatternAlertCommandService(
    IPatternAlertRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : IPatternAlertCommandService
{
    public async Task<Result<PatternAlert>> Handle(CreatePatternAlertCommand command, CancellationToken cancellationToken)
    {
        var entity = new PatternAlert
        {
            PatternId = command.PatternId,
            SectorId = command.SectorId,
            Sector = command.Sector,
            RiskType = command.RiskType,
            OccurrenceCount = command.OccurrenceCount,
            FirstReportDate = command.FirstReportDate,
            Status = command.Status,
            GenerationDate = command.GenerationDate
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PatternAlert>.Success(entity);
        }
        catch (OperationCanceledException) { return Result<PatternAlert>.Failure(PatternAlertError.OperationCancelled, localizer[nameof(PatternAlertError.OperationCancelled)]); }
        catch (DbUpdateException) { return Result<PatternAlert>.Failure(PatternAlertError.DatabaseError, localizer[nameof(PatternAlertError.DatabaseError)]); }
        catch (Exception) { return Result<PatternAlert>.Failure(PatternAlertError.InternalServerError, localizer[nameof(PatternAlertError.InternalServerError)]); }
    }
}
