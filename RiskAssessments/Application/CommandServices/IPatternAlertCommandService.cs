using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.RiskAssessments.Application.CommandServices;

public interface IPatternAlertCommandService
{
    Task<Result<PatternAlert>> Handle(CreatePatternAlertCommand command, CancellationToken cancellationToken);
}
