using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Inspections.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Application.Model;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.Inspections.Application.Internal.Services;

public class InspectionCommandService(AppDbContext context, IUnitOfWork unitOfWork)
{
    private static readonly HashSet<string> ValidUrgencyLevels = ["Bajo", "Medio", "Alto"];
    private static readonly HashSet<string> ValidIncidentTypes =
    [
        "Condicion insegura", "Casi-accidente", "Falla de equipo",
        "Riesgo ergonomico", "Riesgo quimico", "Otro"
    ];

    public async Task<Result<Inspeccion>> CreateAsync(Inspeccion inspection, CancellationToken cancellationToken)
    {
        var validation = await ValidateAsync(inspection, cancellationToken);
        if (validation.IsFailure) return Result<Inspeccion>.Failure(validation.Error!, validation.Message);

        inspection.Ticket = string.IsNullOrWhiteSpace(inspection.Ticket)
            ? $"TICK-{Random.Shared.Next(1000, 9999)}"
            : inspection.Ticket;
        inspection.FechaReporte = DateTime.UtcNow;
        inspection.FechaActualizacion = DateTime.UtcNow;
        inspection.Estado = string.IsNullOrWhiteSpace(inspection.Estado) ? "Pendiente" : inspection.Estado;
        context.Inspecciones.Add(inspection);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Inspeccion>.Success(inspection);
    }

    private async Task<Result> ValidateAsync(Inspeccion inspection, CancellationToken cancellationToken)
    {
        if (!ValidIncidentTypes.Contains(RemoveDiacritics(inspection.TipoIncidente)))
            return Result.Failure("InvalidIncidentType", "Debe seleccionar el tipo de incidente");
        if (!ValidUrgencyLevels.Contains(inspection.NivelUrgencia))
            return Result.Failure("InvalidUrgency", "Debe seleccionar el nivel de urgencia del incidente");
        if (string.IsNullOrWhiteSpace(inspection.Descripcion) || inspection.Descripcion.Length > 300)
            return Result.Failure("InvalidDescription", "La descripcion es obligatoria y debe tener maximo 300 caracteres");

        var areaIsActive = await context.Areas.AnyAsync(area => area.Id == inspection.AreaId && area.Estado == "Activo",
            cancellationToken);
        if (!areaIsActive) return Result.Failure("InvalidArea", "Debe seleccionar un sector activo");

        if (inspection.ActivoId is not null)
        {
            var activeAsset = await context.Activos.AnyAsync(asset =>
                asset.Id == inspection.ActivoId && asset.AreaId == inspection.AreaId && asset.Estado == "Activo",
                cancellationToken);
            if (!activeAsset) return Result.Failure("InvalidAsset", "El activo debe pertenecer al sector activo seleccionado");
        }

        if (!string.IsNullOrWhiteSpace(inspection.FotoUrl) &&
            !(inspection.FotoUrl.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
              inspection.FotoUrl.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
              inspection.FotoUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase)))
            return Result.Failure("InvalidEvidence", "La evidencia debe ser JPG o PNG");

        return Result.Success();
    }

    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);
        foreach (var character in normalized)
            if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
                builder.Append(character);
        return builder.ToString().Normalize(NormalizationForm.FormC);
    }
}
