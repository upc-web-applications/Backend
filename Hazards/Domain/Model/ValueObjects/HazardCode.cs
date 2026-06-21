namespace Acme.Center.Platform.Hazards.Domain.Model.ValueObjects;

public record HazardCode(string Value)
{
    public HazardCode() : this(string.Empty) { }
}
