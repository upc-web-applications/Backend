using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;

namespace Acme.Center.Platform.Mitigations.Application.QueryServices;

public interface IMitigationQueryService
{
    Task<IEnumerable<Mitigation>> Handle(GetAllMitigationsQuery query, CancellationToken cancellationToken);
    Task<Mitigation?> Handle(GetMitigationByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Mitigation>> Handle(GetMitigationsByAssessmentIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Mitigation>> Handle(GetMitigationsByTicketIdQuery query, CancellationToken cancellationToken);
}
