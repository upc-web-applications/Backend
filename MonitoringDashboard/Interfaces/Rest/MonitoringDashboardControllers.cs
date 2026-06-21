using Microsoft.AspNetCore.Mvc;
using Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Interfaces.Rest;

namespace Acme.Center.Platform.MonitoringDashboard.Interfaces.Rest;

[Route("api/v1/heat-map-zones")]
public class HeatMapZonesController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<HeatMapZone>(context, unitOfWork);

[Route("api/v1/dashboard-tickets")]
public class TicketsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Ticket>(context, unitOfWork);

[Route("api/v1/dashboard-technicians")]
public class TechniciansController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Technician>(context, unitOfWork);

[Route("api/v1/dashboard-assets")]
public class AssetsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Asset>(context, unitOfWork);

[Route("api/v1/preventive-maintenances")]
public class PreventiveMaintenancesController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<PreventiveMaintenance>(context, unitOfWork);

[Route("api/v1/archived-reports")]
public class ArchivedReportsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<ArchivedReport>(context, unitOfWork);
