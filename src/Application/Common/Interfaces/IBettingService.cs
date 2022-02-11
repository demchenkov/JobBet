namespace JobBet.Application.Common.Interfaces;

public interface IBettingService
{
    Task MakeBetAsync(int freelancerId, int jobId, decimal price);
}