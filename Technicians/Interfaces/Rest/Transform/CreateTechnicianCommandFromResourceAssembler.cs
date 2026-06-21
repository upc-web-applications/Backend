using Acme.Center.Platform.Technicians.Domain.Model.Commands;
using Acme.Center.Platform.Technicians.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Technicians.Interfaces.Rest.Transform;

public static class CreateTechnicianCommandFromResourceAssembler
{
    public static CreateTechnicianCommand ToCommandFromResource(CreateTechnicianResource resource)
    {
        return new CreateTechnicianCommand(
            resource.DocumentNumber,
            resource.FullName,
            resource.Specialty,
            resource.Phone,
            resource.Email,
            resource.Status);
    }
}
