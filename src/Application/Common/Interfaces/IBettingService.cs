namespace JobBet.Application.Common.Interfaces;

public interface IBettingService
{
    Task MakeBetAsync(int freelancerId, int projectId, decimal price);
}