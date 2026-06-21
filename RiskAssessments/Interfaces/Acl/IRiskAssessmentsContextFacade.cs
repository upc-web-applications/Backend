using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Acl;

public interface IRiskAssessmentsContextFacade
{
    Task<RiskAssessment?> FetchRiskAssessmentById(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<RiskAssessment>> FetchAllRiskAssessments(CancellationToken cancellationToken = default);
}
