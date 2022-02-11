namespace JobBet.Application.Common.Interfaces;

public interface IJobService
{
    Task<bool> IsUserIsJobOwnerAsync(int jobId, string userId);
    Task<bool> IsUserIsJobExecutorAsync(int jobId, string userId);
    
    Task<bool> IsCurrentUserIsJobOwnerAsync(int jobId);
    
    Task<bool> IsCurrentUserIsJobExecutorAsync(int jobId);
}