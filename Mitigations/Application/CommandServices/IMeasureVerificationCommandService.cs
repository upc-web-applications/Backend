using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.Mitigations.Application.CommandServices;

public interface IMeasureVerificationCommandService
{
    Task<Result<MeasureVerification>> Handle(CreateMeasureVerificationCommand command, CancellationToken cancellationToken);
}
