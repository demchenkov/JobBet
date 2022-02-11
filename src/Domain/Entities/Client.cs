namespace JobBet.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    
    public string Title { get; set; } = default!;
    
    public string UserId { get; set; } = default!;

    public List<Job> Jobs { get; } = new List<Job>();
    
    public ClientAverageRating? Rating { get; set; }
}