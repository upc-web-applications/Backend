using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Acme.Center.Platform.Technicians.Application.CommandServices;
using Acme.Center.Platform.Technicians.Domain.Model;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Domain.Model.Commands;
using Acme.Center.Platform.Technicians.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Resources.Errors;

namespace Acme.Center.Platform.Technicians.Application.Internal.CommandServices;

public class TechnicianCommandService(
    ITechnicianRepository technicianRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : ITechnicianCommandService
{
    public async Task<Result<Technician>> Handle(CreateTechnicianCommand command, CancellationToken cancellationToken)
    {
        var technician = new Technician
        {
            DocumentNumber = command.DocumentNumber,
            FullName = command.FullName,
            Specialty = command.Specialty,
            Phone = command.Phone,
            Email = command.Email,
            Status = command.Status
        };
        try
        {
            await technicianRepository.AddAsync(technician, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Technician>.Success(technician);
        }
        catch (OperationCanceledException)
        {
            return Result<Technician>.Failure(TechnicianError.OperationCancelled, localizer[nameof(TechnicianError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Technician>.Failure(TechnicianError.DatabaseError, localizer[nameof(TechnicianError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Technician>.Failure(TechnicianError.InternalServerError, localizer[nameof(TechnicianError.InternalServerError)]);
        }
    }
}
