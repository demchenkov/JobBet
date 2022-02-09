namespace JobBet.Domain.Entities;

public class ProjectRating
{
    public int Id { get; set; }

    public int? ClientScore { get; set; }
    
    public int? FreelancerScore { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
}