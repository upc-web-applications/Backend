using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;

namespace RiskGuard.Platform.MonitoringDashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMonitoringDashboardConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HeatMapZone>().HasKey(zone => zone.Id);
        builder.Entity<HeatMapZone>().Property(zone => zone.Id).ValueGeneratedOnAdd();
        builder.Entity<Ticket>().HasKey(ticket => ticket.Id);
        builder.Entity<Ticket>().Property(ticket => ticket.Id).ValueGeneratedOnAdd();
        builder.Entity<Technician>().HasKey(technician => technician.Id);
        builder.Entity<Asset>().HasKey(asset => asset.Id);
        builder.Entity<PreventiveMaintenance>().HasKey(maintenance => maintenance.Id);
        builder.Entity<ArchivedReport>().HasKey(report => report.Id);
    }
}
