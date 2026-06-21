using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.Technicians.Application.CommandServices;

public interface ITechnicianCommandService
{
    Task<Result<Technician>> Handle(CreateTechnicianCommand command, CancellationToken cancellationToken);
}
