using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.Mitigations.Application.CommandServices;

public interface ISlaAlertCommandService
{
    Task<Result<SlaAlert>> Handle(CreateSlaAlertCommand command, CancellationToken cancellationToken);
}
