namespace Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreateHistoricalIncidentRecordCommand(string Sector, string IncidentType, string Criticality);
