using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Interfaces.Rest;

namespace Acme.Center.Platform.MonitoringDashboard.Interfaces.Rest;

[Route("api/v1/heat-map-zones")]
[Authorize(Policy = "SupervisorOnly")]
public class HeatMapZonesController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<HeatMapZone>(context, unitOfWork);

[Route("api/v1/dashboard-tickets")]
[Authorize(Policy = "SupervisorOnly")]
public class TicketsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Ticket>(context, unitOfWork);

[Route("api/v1/dashboard-technicians")]
[Authorize(Policy = "SupervisorOnly")]
public class TechniciansController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Technician>(context, unitOfWork);

[Route("api/v1/dashboard-assets")]
[Authorize(Policy = "SupervisorOnly")]
public class AssetsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Asset>(context, unitOfWork);

[Route("api/v1/preventive-maintenances")]
[Authorize(Policy = "SupervisorOnly")]
public class PreventiveMaintenancesController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<PreventiveMaintenance>(context, unitOfWork);

[Route("api/v1/archived-reports")]
[Authorize(Policy = "SupervisorOnly")]
public class ArchivedReportsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<ArchivedReport>(context, unitOfWork);
