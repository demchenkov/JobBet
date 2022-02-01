using JobBet.Application.Common.Models;
using JobBet.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobBet.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler : INotificationHandler<DomainEventNotification<TodoItemCompletedEvent>>
{
    private readonly ILogger<TodoItemCompletedEventHandler> _logger;

    public TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<TodoItemCompletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("JobBet Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
