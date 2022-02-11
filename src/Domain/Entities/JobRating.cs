namespace JobBet.Domain.Entities;

public class JobRating
{
    public int Id { get; set; }

    public int? ClientScore { get; set; }
    
    public int? FreelancerScore { get; set; }
    
    public int JobId { get; set; }
    public Job Job { get; set; } = default!;
}