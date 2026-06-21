using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Inspections.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace Acme.Center.Platform.Inspections.Application.Internal.Services;

public class InspectionCommandService(AppDbContext context, IUnitOfWork unitOfWork)
{
    private static readonly HashSet<string> ValidUrgencyLevels = ["Low", "Medium", "High"];
    private static readonly HashSet<string> ValidIncidentTypes =
    [
        "Unsafe condition", "Near-miss", "Equipment failure",
        "Ergonomic risk", "Chemical risk", "Other"
    ];

    public async Task<Result<Inspection>> CreateAsync(Inspection inspection, CancellationToken cancellationToken)
    {
        var validation = await ValidateAsync(inspection, cancellationToken);
        if (validation.IsFailure) return Result<Inspection>.Failure(validation.Error!, validation.Message);

        inspection.Ticket = string.IsNullOrWhiteSpace(inspection.Ticket)
            ? $"TICK-{Random.Shared.Next(1000, 9999)}"
            : inspection.Ticket;
        inspection.ReportDate = DateTime.UtcNow;
        inspection.UpdateDate = DateTime.UtcNow;
        inspection.Status = string.IsNullOrWhiteSpace(inspection.Status) ? "Pending" : inspection.Status;
        context.Inspections.Add(inspection);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Inspection>.Success(inspection);
    }

    private async Task<Result> ValidateAsync(Inspection inspection, CancellationToken cancellationToken)
    {
        if (!ValidIncidentTypes.Contains(RemoveDiacritics(inspection.IncidentType)))
            return Result.Failure("InvalidIncidentType", "You must select the incident type");
        if (!ValidUrgencyLevels.Contains(inspection.UrgencyLevel))
            return Result.Failure("InvalidUrgency", "You must select the urgency level");
        if (string.IsNullOrWhiteSpace(inspection.Description) || inspection.Description.Length > 300)
            return Result.Failure("InvalidDescription", "Description is required and must be at most 300 characters");

        var areaIsActive = await context.Areas.AnyAsync(area => area.Id == inspection.AreaId && area.Status == "Active",
            cancellationToken);
        if (!areaIsActive) return Result.Failure("InvalidArea", "You must select an active area");

        if (inspection.AssetId is not null)
        {
            var activeAsset = await context.OrgAssets.AnyAsync(asset =>
                asset.Id == inspection.AssetId && asset.AreaId == inspection.AreaId && asset.Status == "Active",
                cancellationToken);
            if (!activeAsset) return Result.Failure("InvalidAsset", "The asset must belong to the selected active area");
        }

        if (!string.IsNullOrWhiteSpace(inspection.PhotoUrl) &&
            !(inspection.PhotoUrl.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
              inspection.PhotoUrl.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
              inspection.PhotoUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase)))
            return Result.Failure("InvalidEvidence", "Photo evidence must be JPG or PNG");

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
