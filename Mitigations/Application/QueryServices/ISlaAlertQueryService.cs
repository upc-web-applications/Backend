using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;

namespace Acme.Center.Platform.Mitigations.Application.QueryServices;

public interface ISlaAlertQueryService
{
    Task<IEnumerable<SlaAlert>> Handle(GetAllSlaAlertsQuery query, CancellationToken cancellationToken);
    Task<SlaAlert?> Handle(GetSlaAlertByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<SlaAlert>> Handle(GetAlertsByTicketQuery query, CancellationToken cancellationToken);
}
