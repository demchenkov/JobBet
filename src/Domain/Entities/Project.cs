namespace JobBet.Domain.Entities;

public class Project : AuditableEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; } = ExperienceLevel.Entry;
    
    public ProjectType? ProjectType { get; set; }
    
    public decimal Price { get; set; }

    public int? FreelancerId { get; set; }
    public Freelancer Freelancer { get; set; } = null!;
}