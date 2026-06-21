using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace RiskGuard.Platform.Shared.Interfaces.Rest;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class CrudController<TEntity>(AppDbContext context, IUnitOfWork unitOfWork) : ControllerBase
    where TEntity : class, new()
{
    [HttpGet]
    [SwaggerOperation("Get all resources", "Get all resources for this endpoint.")]
    public virtual async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get resource by id", "Get one resource by its identifier.")]
    public virtual async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var entity = await context.Set<TEntity>().FindAsync([ConvertId(id)], cancellationToken);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [SwaggerOperation("Create resource", "Create a resource for this endpoint.")]
    public virtual async Task<IActionResult> Create([FromBody] TEntity resource, CancellationToken cancellationToken)
    {
        await context.Set<TEntity>().AddAsync(resource, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        var id = GetId(resource);
        return CreatedAtAction(nameof(GetById), new { id }, resource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update resource", "Update a resource for this endpoint.")]
    public virtual async Task<IActionResult> Update(string id, [FromBody] TEntity resource,
        CancellationToken cancellationToken)
    {
        SetId(resource, id);
        context.Entry(resource).State = EntityState.Modified;
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(resource);
    }

    [HttpPatch("{id}")]
    [SwaggerOperation("Patch resource", "Patch currently behaves as a full update for frontend compatibility.")]
    public virtual async Task<IActionResult> Patch(string id, [FromBody] TEntity resource,
        CancellationToken cancellationToken)
    {
        return await Update(id, resource, cancellationToken);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete resource", "Delete a resource for this endpoint.")]
    public virtual async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var entity = await context.Set<TEntity>().FindAsync([ConvertId(id)], cancellationToken);
        if (entity is null) return NotFound();
        context.Set<TEntity>().Remove(entity);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }

    protected static object ConvertId(string id)
    {
        var property = typeof(TEntity).GetProperty("Id")
                       ?? throw new InvalidOperationException($"{typeof(TEntity).Name} must have an Id property.");
        if (property.PropertyType == typeof(int)) return int.Parse(id);
        if (property.PropertyType == typeof(long)) return long.Parse(id);
        if (property.PropertyType == typeof(Guid)) return Guid.Parse(id);
        return id;
    }

    private static object? GetId(TEntity entity)
    {
        return typeof(TEntity).GetProperty("Id")?.GetValue(entity);
    }

    private static void SetId(TEntity entity, string id)
    {
        var property = typeof(TEntity).GetProperty("Id");
        if (property is null || !property.CanWrite) return;
        property.SetValue(entity, ConvertId(id));
    }
}
