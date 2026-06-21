using Microsoft.Extensions.DependencyInjection;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.Shared.Infrastructure.Mediator.InProcess;

public class EventDispatcher(IServiceProvider serviceProvider)
{
    public async Task DispatchAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        using var scope = serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            await handler.Handle(domainEvent, cancellationToken);
        }
    }
}
