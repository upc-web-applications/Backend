namespace Acme.Center.Platform.Technicians.Domain.Model.ValueObjects;

public record EmailAddress(string Address)
{
    public EmailAddress() : this(string.Empty) { }
}
