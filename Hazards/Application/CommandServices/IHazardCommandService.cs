using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.Hazards.Application.CommandServices;

public interface IHazardCommandService
{
    Task<Result<Hazard>> Handle(CreateHazardCommand command, CancellationToken cancellationToken);
}
