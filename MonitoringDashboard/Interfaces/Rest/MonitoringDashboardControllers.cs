using Microsoft.AspNetCore.Mvc;
using RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Interfaces.Rest;

namespace RiskGuard.Platform.MonitoringDashboard.Interfaces.Rest;

[Route("api/v1/heatMapZones")]
public class HeatMapZonesController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<HeatMapZone>(context, unitOfWork);

[Route("api/v1/tickets")]
public class TicketsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Ticket>(context, unitOfWork);

[Route("api/v1/technicians")]
public class TechniciansController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Technician>(context, unitOfWork);

[Route("api/v1/assets")]
public class AssetsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Asset>(context, unitOfWork);

[Route("api/v1/preventiveMaintenances")]
public class PreventiveMaintenancesController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<PreventiveMaintenance>(context, unitOfWork);

[Route("api/v1/archivedReports")]
public class ArchivedReportsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<ArchivedReport>(context, unitOfWork);
