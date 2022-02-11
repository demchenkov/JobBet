namespace JobBet.Domain.Entities;

public class TalentAuction : Auction<int, Freelancer>
{
    public bool IsActive { get; set; } = true;
}