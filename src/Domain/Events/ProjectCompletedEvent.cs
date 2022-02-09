namespace JobBet.Domain.Events;

public class ProjectCompletedEvent : DomainEvent
{
    public ProjectCompletedEvent(Project project)
    {
        Project = project;
    }

    public Project Project { get; }
}