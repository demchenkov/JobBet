using JobBet.Domain.Common;

namespace JobBet.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
