using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;
using Acme.Center.Platform.RiskAssessments.Interfaces.Acl;

namespace Acme.Center.Platform.RiskAssessments.Application.Acl;

public class RiskAssessmentsContextFacade(IRiskAssessmentQueryService queryService) : IRiskAssessmentsContextFacade
{
    public async Task<RiskAssessment?> FetchRiskAssessmentById(string id, CancellationToken cancellationToken = default)
    {
        var query = new GetRiskAssessmentByIdQuery(id);
        return await queryService.Handle(query, cancellationToken);
    }

    public async Task<IEnumerable<RiskAssessment>> FetchAllRiskAssessments(CancellationToken cancellationToken = default)
    {
        var query = new GetAllRiskAssessmentsQuery();
        return await queryService.Handle(query, cancellationToken);
    }
}
