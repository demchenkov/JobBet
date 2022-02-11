namespace JobBet.Domain.Entities;

public class LikedJob
{
    public int FreelancerId { get; set; }
    public Freelancer Freelancer { get; set; } = default!;

    public int JobId { get; set; }
    public Job Job { get; set; } = default!;
}