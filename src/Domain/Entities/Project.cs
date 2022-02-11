namespace JobBet.Domain.Entities;

public class Project : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; } = ExperienceLevel.Entry;
    
    public ProjectType? ProjectType { get; set; }

    #region StatusProperty

    private ProjectStatus _status = ProjectStatus.Created;
    public ProjectStatus Status
    {
        get => _status;
        set
        {
            if (_status != ProjectStatus.Done && value == ProjectStatus.Done)
            {
                DomainEvents.Add(new ProjectCompletedEvent(this));
            }

            _status = value;
        }
    }

    #endregion
    
    public int ClientId { get; set; }
    public Client Client { get; set; } = default!;
    
    public int? ExecutorId { get; set; }
    public Freelancer? Executor { get; set; } = null!;

    public ProjectAuction? Auction { get; set; }

    public List<DomainEvent> DomainEvents { get; set; }  = new List<DomainEvent>();
}