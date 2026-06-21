using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Acme.Center.Platform.Hazards.Application.CommandServices;
using Acme.Center.Platform.Hazards.Domain.Model;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Domain.Model.Commands;
using Acme.Center.Platform.Hazards.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Resources.Errors;

namespace Acme.Center.Platform.Hazards.Application.Internal.CommandServices;

public class HazardCommandService(
    IHazardRepository hazardRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : IHazardCommandService
{
    public async Task<Result<Hazard>> Handle(CreateHazardCommand command, CancellationToken cancellationToken)
    {
        var hazard = new Hazard
        {
            Code = command.Code,
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            BaseRiskLevel = command.BaseRiskLevel,
            Status = command.Status
        };
        try
        {
            await hazardRepository.AddAsync(hazard, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Hazard>.Success(hazard);
        }
        catch (OperationCanceledException)
        {
            return Result<Hazard>.Failure(HazardError.OperationCancelled, localizer[nameof(HazardError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Hazard>.Failure(HazardError.DatabaseError, localizer[nameof(HazardError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Hazard>.Failure(HazardError.InternalServerError, localizer[nameof(HazardError.InternalServerError)]);
        }
    }
}
