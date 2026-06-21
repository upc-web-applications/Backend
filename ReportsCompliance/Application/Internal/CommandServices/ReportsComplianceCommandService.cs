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
    public async Task<Result<MonthlyReport>> Handle(CreateMonthlyReportCommand command, CancellationToken cancellationToken)
    {
        var entity = new MonthlyReport { Month = command.Month, Year = command.Year };
        try
        {
            await context.MonthlyReports.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<MonthlyReport>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<MonthlyReport>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

    public async Task<Result<CumulativeStIndicator>> Handle(CreateCumulativeStIndicatorCommand command, CancellationToken cancellationToken)
    {
        var entity = new CumulativeStIndicator { Name = command.Name, Value = command.Value, Status = command.Status };
        try
        {
            await context.CumulativeStIndicators.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<CumulativeStIndicator>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<CumulativeStIndicator>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

    public async Task<Result<HistoricalIncidentRecord>> Handle(CreateHistoricalIncidentRecordCommand command, CancellationToken cancellationToken)
    {
        var entity = new HistoricalIncidentRecord { Sector = command.Sector, IncidentType = command.IncidentType, Criticality = command.Criticality };
        try
        {
            await context.HistoricalIncidentRecords.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HistoricalIncidentRecord>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<HistoricalIncidentRecord>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

    public async Task<Result<AnnualOhsPlan>> Handle(CreateAnnualOhsPlanCommand command, CancellationToken cancellationToken)
    {
        var entity = new AnnualOhsPlan
        {
            Year = command.Year, GlobalCompliance = command.GlobalCompliance, Goal = command.Goal,
            CompletedActivities = command.CompletedActivities, TotalActivities = command.TotalActivities
        };
        try
        {
            await context.AnnualOhsPlans.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<AnnualOhsPlan>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<AnnualOhsPlan>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

    public async Task<Result<PredictiveIndicator>> Handle(CreatePredictiveIndicatorCommand command, CancellationToken cancellationToken)
    {
        var entity = new PredictiveIndicator { Name = command.Name, Description = command.Description, Value = command.Value, Trend = command.Trend };
        try
        {
            await context.PredictiveIndicators.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<PredictiveIndicator>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<PredictiveIndicator>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

    public async Task<Result<CriticalAlert>> Handle(CreateCriticalAlertCommand command, CancellationToken cancellationToken)
    {
        var entity = new CriticalAlert
        {
            Type = command.Type, Sector = command.Sector, RiskType = command.RiskType,
            Message = command.Message, ElapsedHours = command.ElapsedHours
        };
        try
        {
            await context.CriticalAlerts.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<CriticalAlert>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<CriticalAlert>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

    public async Task<Result<GeneratedReport>> Handle(CreateGeneratedReportCommand command, CancellationToken cancellationToken)
    {
        var entity = new GeneratedReport
        {
            Type = command.Type, Month = command.Month, Year = command.Year,
            Format = command.Format, FileName = command.FileName
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

    public async Task<Result<KpiDashboard>> Handle(CreateKpiDashboardCommand command, CancellationToken cancellationToken)
    {
        var entity = new KpiDashboard { Name = command.Name, Value = command.Value, Goal = command.Goal, Status = command.Status };
        try
        {
            await context.KpiDashboard.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<KpiDashboard>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<KpiDashboard>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }

    public async Task<Result<HistoricalTrend>> Handle(CreateHistoricalTrendCommand command, CancellationToken cancellationToken)
    {
        var entity = new HistoricalTrend
        {
            Month = command.Month, Year = command.Year, TotalIncidents = command.TotalIncidents,
            Sector = command.Sector, Type = command.Type
        };
        try
        {
            await context.HistoricalTrends.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HistoricalTrend>.Success(entity);
        }
        catch (DbUpdateException)
        {
            return Result<HistoricalTrend>.Failure(ReportsComplianceError.DatabaseError, localizer[nameof(ReportsComplianceError.DatabaseError)]);
        }
    }
}
