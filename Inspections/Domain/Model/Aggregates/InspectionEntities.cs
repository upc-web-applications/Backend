namespace RiskGuard.Platform.Inspections.Domain.Model.Aggregates;

public class Inspeccion
{
    public int Id { get; set; }
    public string Ticket { get; set; } = string.Empty;
    public string TipoIncidente { get; set; } = string.Empty;
    public int AreaId { get; set; }
    public int SedeId { get; set; }
    public int? ActivoId { get; set; }
    public string NivelUrgencia { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = "Pendiente";
    public string? FotoUrl { get; set; }
    public string OperarioId { get; set; } = string.Empty;
    public DateTime FechaReporte { get; set; } = DateTime.UtcNow;
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    public string? AccionCorrectiva { get; set; }
}

public class Peligro
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class EvidenciaFotografica
{
    public int Id { get; set; }
    public int InspeccionId { get; set; }
    public string UrlArchivo { get; set; } = string.Empty;
    public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
}
