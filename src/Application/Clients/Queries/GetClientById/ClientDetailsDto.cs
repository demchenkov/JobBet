namespace JobBet.Application.Clients.Queries.GetClientById;

public class ClientDetailsDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = default!;
}