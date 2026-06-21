namespace RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;

public class Sede
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Estado { get; set; } = "Activo";
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}

public class Area
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int SedeId { get; set; }
    public string Estado { get; set; } = "Activo";
    public string NivelRiesgo { get; set; } = "Medio";
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}

public class Activo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string NumeroSerie { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int AreaId { get; set; }
    public int SedeId { get; set; }
    public string Estado { get; set; } = "Activo";
    public DateTime FechaIngresoSistema { get; set; } = DateTime.UtcNow;
    public DateTime? FechaBaja { get; set; }
}
