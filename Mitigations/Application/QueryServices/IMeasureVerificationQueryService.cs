using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;

namespace Acme.Center.Platform.Mitigations.Application.QueryServices;

public interface IMeasureVerificationQueryService
{
    Task<IEnumerable<MeasureVerification>> Handle(GetAllMeasureVerificationsQuery query, CancellationToken cancellationToken);
    Task<MeasureVerification?> Handle(GetMeasureVerificationByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<MeasureVerification>> Handle(GetVerificationsByTicketQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<MeasureVerification>> Handle(GetVerificationsByVerdictQuery query, CancellationToken cancellationToken);
}
