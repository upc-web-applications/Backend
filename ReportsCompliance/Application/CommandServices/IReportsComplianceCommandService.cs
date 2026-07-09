using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.ReportsCompliance.Application.CommandServices;

public interface IReportsComplianceCommandService
{
    Task<Result<GeneratedReport>> Handle(CreateGeneratedReportCommand command, CancellationToken cancellationToken);
}
