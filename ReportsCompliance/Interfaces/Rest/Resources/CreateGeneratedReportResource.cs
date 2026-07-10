using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateGeneratedReportResource(
    string Type, int? Month, int? Year, string Format,
    [property: JsonPropertyName("file_name")] string FileName,
    [property: JsonPropertyName("start_date")] DateTime? StartDate,
    [property: JsonPropertyName("end_date")] DateTime? EndDate,
    [property: JsonPropertyName("sector_filter")] string? SectorFilter,
    [property: JsonPropertyName("size_kb")] int? SizeKb);
