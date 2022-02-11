namespace JobBet.Domain.Events;

public class JobCompletedEvent : DomainEvent
{
    public JobCompletedEvent(Job project)
    {
        Job = project;
    }

    public Job Job { get; }
}