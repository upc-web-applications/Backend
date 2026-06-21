using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Inspections.Application.Internal.Services;
using RiskGuard.Platform.Inspections.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Interfaces.Rest;

namespace RiskGuard.Platform.Inspections.Interfaces.Rest;

[Route("api/v1/inspecciones")]
public class InspeccionesController(
    AppDbContext context,
    IUnitOfWork unitOfWork,
    InspectionCommandService inspectionCommandService)
    : CrudController<Inspeccion>(context, unitOfWork)
{
    [HttpPost]
    public override async Task<IActionResult> Create([FromBody] Inspeccion resource, CancellationToken cancellationToken)
    {
        var result = await inspectionCommandService.CreateAsync(resource, cancellationToken);
        return result.IsFailure
            ? BadRequest(new { message = result.Message, error = result.Error })
            : CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpGet("mine/{operarioId}")]
    public async Task<IActionResult> GetMine(string operarioId, [FromQuery] string? estado,
        CancellationToken cancellationToken)
    {
        var query = context.Inspecciones.Where(item => item.OperarioId == operarioId);
        if (!string.IsNullOrWhiteSpace(estado)) query = query.Where(item => item.Estado == estado);
        return Ok(await query.OrderByDescending(item => item.FechaReporte).ToListAsync(cancellationToken));
    }
}

[Route("api/v1/peligros")]
public class PeligrosController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Peligro>(context, unitOfWork);
