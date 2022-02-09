using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Models;
using JobBet.Domain.Entities;
using JobBet.Domain.Events;
using MediatR;

namespace JobBet.Application.Rating.EventHandlers;

public class ProjectCompletedEventHandler : INotificationHandler<DomainEventNotification<ProjectCompletedEvent>>
{
    private readonly IApplicationDbContext _context;

    public ProjectCompletedEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DomainEventNotification<ProjectCompletedEvent> notification, CancellationToken cancellationToken)
    {
        var @event = notification.DomainEvent;
        var entity = new ProjectRating { ProjectId = @event.Project.Id };
        
        _context.ProjectRatings.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}