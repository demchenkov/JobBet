namespace JobBet.Domain.Entities;

public class Auction
{
    public int Id { get; set; }
    
    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset EndTime { get; set; }
    
    public decimal InitialPrice { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
}