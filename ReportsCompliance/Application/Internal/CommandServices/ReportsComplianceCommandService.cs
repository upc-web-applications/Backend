using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Acme.Center.Platform.ReportsCompliance.Application.CommandServices;
using Acme.Center.Platform.ReportsCompliance.Domain.Model;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Resources.Errors;

namespace Acme.Center.Platform.ReportsCompliance.Application.Internal.CommandServices;

public class ReportsComplianceCommandService(AppDbContext context, IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessage> localizer)
    : IReportsComplianceCommandService
{
    public async Task<Result<GeneratedReport>> Handle(CreateGeneratedReportCommand command, CancellationToken cancellationToken)
    {
        var entity = new GeneratedReport
        {
            Type = command.Type, Month = command.Month, Year = command.Year,
            Format = command.Format, FileName = command.FileName,
            StartDate = command.StartDate, EndDate = command.EndDate,
            SectorFilter = command.SectorFilter, SizeKb = command.SizeKb
        };
        try
        {
            await context.GeneratedReports.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<GeneratedReport>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<GeneratedReport>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

}
