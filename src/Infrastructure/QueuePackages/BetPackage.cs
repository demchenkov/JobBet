namespace JobBet.Infrastructure.QueuePackages;

public class BetPackage
{
    public int FreelancerId { get; set; }

    public int ProjectId { get; set; }
    
    public decimal Price { get; set; }
}