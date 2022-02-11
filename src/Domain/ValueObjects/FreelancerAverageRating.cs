namespace JobBet.Domain.ValueObjects;

public class FreelancerAverageRating
{
    public double Score { get; set; }
    
    public int FreelancerId { get; set; }
    public Freelancer Freelancer { get; set; } = default!;
}