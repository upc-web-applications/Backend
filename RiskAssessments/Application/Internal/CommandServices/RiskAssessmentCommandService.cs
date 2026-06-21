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

public class RiskAssessmentCommandService(
    IRiskAssessmentRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : IRiskAssessmentCommandService
{
    public async Task<Result<RiskAssessment>> Handle(CreateRiskAssessmentCommand command, CancellationToken cancellationToken)
    {
        var entity = new RiskAssessment
        {
            Code = command.Code,
            Sector = command.Sector,
            HazardType = command.HazardType,
            Description = command.Description,
            Probability = command.Probability,
            Severity = command.Severity,
            RiskLevel = command.RiskLevel,
            ControlMeasures = command.ControlMeasures,
            Status = command.Status,
            EvaluationDate = command.EvaluationDate,
            UserId = command.UserId
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<RiskAssessment>.Success(entity);
        }
        catch (OperationCanceledException) { return Result<RiskAssessment>.Failure(RiskAssessmentError.OperationCancelled, localizer[nameof(RiskAssessmentError.OperationCancelled)]); }
        catch (DbUpdateException) { return Result<RiskAssessment>.Failure(RiskAssessmentError.DatabaseError, localizer[nameof(RiskAssessmentError.DatabaseError)]); }
        catch (Exception) { return Result<RiskAssessment>.Failure(RiskAssessmentError.InternalServerError, localizer[nameof(RiskAssessmentError.InternalServerError)]); }
    }
}
