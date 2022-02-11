namespace JobBet.Domain.Entities;

public class Job : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; } = ExperienceLevel.Entry;

    public JobType? JobType { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = default!;

    public int? ExecutorId { get; set; }
    public Freelancer? Executor { get; set; } = null!;

    public JobAuction? Auction { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    #region StatusProperty

    private JobStatus _status = JobStatus.Created;

    public JobStatus Status
    {
        get => _status;
        set
        {
            if (_status != JobStatus.Done && value == JobStatus.Done)
            {
                DomainEvents.Add(new JobCompletedEvent(this));
            }

            _status = value;
        }
    }

    #endregion
}