using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Application.Internal.QueryServices;

public class RiskAssessmentQueryService(IRiskAssessmentRepository repository) : IRiskAssessmentQueryService
{
    public async Task<IEnumerable<RiskAssessment>> Handle(GetAllRiskAssessmentsQuery query, CancellationToken cancellationToken)
        => await repository.ListAsync(cancellationToken);

    public async Task<RiskAssessment?> Handle(GetRiskAssessmentByIdQuery query, CancellationToken cancellationToken)
        => await repository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<RiskAssessment>> Handle(GetRiskAssessmentsBySectorQuery query, CancellationToken cancellationToken)
        => await repository.FindBySectorAsync(query.Sector, cancellationToken);
}
