namespace Acme.Center.Platform.Technicians.Domain.Model.ValueObjects;

public record PhoneNumber(string Value)
{
    public PhoneNumber() : this(string.Empty) { }
}
