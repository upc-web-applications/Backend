using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Inspections.Application.Internal.Services;
using Acme.Center.Platform.Inspections.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Interfaces.Rest;

namespace Acme.Center.Platform.Inspections.Interfaces.Rest;

[Route("api/v1/inspections")]
public class InspectionsController(
    AppDbContext context,
    IUnitOfWork unitOfWork,
    InspectionCommandService inspectionCommandService)
    : CrudController<Inspection>(context, unitOfWork)
{
    [HttpPost]
    public override async Task<IActionResult> Create([FromBody] Inspection resource, CancellationToken cancellationToken)
    {
        var result = await inspectionCommandService.CreateAsync(resource, cancellationToken);
        return result.IsFailure
            ? BadRequest(new { message = result.Message, error = result.Error })
            : CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpGet("mine/{operatorId}")]
    public async Task<IActionResult> GetMine(string operatorId, [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var query = context.Inspections.Where(item => item.OperatorId == operatorId);
        if (!string.IsNullOrWhiteSpace(status)) query = query.Where(item => item.Status == status);
        return Ok(await query.OrderByDescending(item => item.ReportDate).ToListAsync(cancellationToken));
    }
}

[Route("api/v1/dangers")]
public class DangersController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Danger>(context, unitOfWork);
