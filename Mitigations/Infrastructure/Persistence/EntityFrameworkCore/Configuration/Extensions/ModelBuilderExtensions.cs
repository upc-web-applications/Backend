using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMitigationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Mitigation>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Code).HasMaxLength(50); e.Property(x => x.Status).HasMaxLength(50); });
        builder.Entity<CorrectiveActionTicket>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Sector).HasMaxLength(200); e.Property(x => x.RiskType).HasMaxLength(100); e.Property(x => x.CriticalityLevel).HasMaxLength(50); e.Property(x => x.Status).HasMaxLength(50); e.Property(x => x.TechnicianName).HasMaxLength(200); });
        builder.Entity<MeasureVerification>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.SupervisorName).HasMaxLength(200); e.Property(x => x.Verdict).HasMaxLength(50); });
        builder.Entity<TicketHistory>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Event).HasMaxLength(100); e.Property(x => x.UserName).HasMaxLength(200); });
        builder.Entity<SlaAlert>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.NotifiedName).HasMaxLength(200); });
        builder.Entity<CriticalNotification>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.SupervisorName).HasMaxLength(200); e.Property(x => x.Message).HasMaxLength(500); });
    }
}
