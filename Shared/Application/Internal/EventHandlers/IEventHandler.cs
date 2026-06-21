using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
}
