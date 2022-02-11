using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Models;
using JobBet.Domain.Entities;
using JobBet.Domain.Events;
using MediatR;

namespace JobBet.Application.Rating.EventHandlers;

public class JobCompletedEventHandler : INotificationHandler<DomainEventNotification<JobCompletedEvent>>
{
    private readonly IApplicationDbContext _context;

    public JobCompletedEventHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DomainEventNotification<JobCompletedEvent> notification,
        CancellationToken cancellationToken)
    {
        JobCompletedEvent @event = notification.DomainEvent;
        JobRating entity = new JobRating {JobId = @event.Job.Id};

        _context.JobRatings.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}