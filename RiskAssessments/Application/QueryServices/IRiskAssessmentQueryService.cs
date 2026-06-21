using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;

namespace Acme.Center.Platform.RiskAssessments.Application.QueryServices;

public interface IRiskAssessmentQueryService
{
    Task<IEnumerable<RiskAssessment>> Handle(GetAllRiskAssessmentsQuery query, CancellationToken cancellationToken);
    Task<RiskAssessment?> Handle(GetRiskAssessmentByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<RiskAssessment>> Handle(GetRiskAssessmentsBySectorQuery query, CancellationToken cancellationToken);
}
