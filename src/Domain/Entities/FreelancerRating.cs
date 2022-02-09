namespace JobBet.Domain.Entities;

public class FreelancerRating
{
    public int FreelancerId { get; set; }
    
    public double Rating { get; set; }

    public Freelancer Freelancer { get; set; } = default!;
}