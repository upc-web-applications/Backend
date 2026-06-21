namespace Acme.Center.Platform.Technicians.Domain.Model.ValueObjects;

public record DocumentNumber(string Value)
{
    public DocumentNumber() : this(string.Empty) { }
}
