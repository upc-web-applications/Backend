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

public class AreaCriticalityLevelCommandService(
    IAreaCriticalityLevelRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : IAreaCriticalityLevelCommandService
{
    public async Task<Result<AreaCriticalityLevel>> Handle(CreateAreaCriticalityLevelCommand command, CancellationToken cancellationToken)
    {
        var entity = new AreaCriticalityLevel
        {
            SectorId = command.SectorId,
            Sector = command.Sector,
            CriticalityLevel = command.CriticalityLevel,
            MapIntensity = command.MapIntensity,
            LastUpdated = command.LastUpdated
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<AreaCriticalityLevel>.Success(entity);
        }
        catch (OperationCanceledException) { return Result<AreaCriticalityLevel>.Failure(AreaCriticalityLevelError.OperationCancelled, localizer[nameof(AreaCriticalityLevelError.OperationCancelled)]); }
        catch (DbUpdateException) { return Result<AreaCriticalityLevel>.Failure(AreaCriticalityLevelError.DatabaseError, localizer[nameof(AreaCriticalityLevelError.DatabaseError)]); }
        catch (Exception) { return Result<AreaCriticalityLevel>.Failure(AreaCriticalityLevelError.InternalServerError, localizer[nameof(AreaCriticalityLevelError.InternalServerError)]); }
    }
}
