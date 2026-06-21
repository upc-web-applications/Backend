using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Technicians.Interfaces.Rest.Transform;

public static class TechnicianResourceFromEntityAssembler
{
    public static TechnicianResource ToResourceFromEntity(Technician entity)
    {
        return new TechnicianResource(
            entity.Id,
            entity.DocumentNumber,
            entity.FullName,
            entity.Specialty,
            entity.Phone,
            entity.Email,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
