namespace JobBet.Domain.Entities;

public class Project : AuditableEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; } = ExperienceLevel.Entry;
    
    public ProjectType? ProjectType { get; set; }
    
    public int ClientId { get; set; }
    public Client Client { get; set; } = default!;
    
    public int ExecutorId { get; set; }
    public Freelancer Executor { get; set; } = null!;

    public Auction? Auction { get; set; }
}