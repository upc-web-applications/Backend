using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record GeneratedReportResource(
    string Id, string Type, int? Month, int? Year, string Format,
    [property: JsonPropertyName("generation_date")] DateTime GenerationDate,
    [property: JsonPropertyName("file_name")] string FileName,
    string Status,
    [property: JsonPropertyName("start_date")] DateTime? StartDate,
    [property: JsonPropertyName("end_date")] DateTime? EndDate,
    [property: JsonPropertyName("sector_filter")] string? SectorFilter,
    [property: JsonPropertyName("size_kb")] int? SizeKb);
