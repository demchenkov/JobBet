namespace JobBet.Application.Common.Interfaces;

public interface IProjectService
{
    Task<bool> IsUserIsProjectOwnerAsync(int projectId, string userId);
    Task<bool> IsUserIsProjectExecutorAsync(int projectId, string userId);
    
    Task<bool> IsCurrentUserIsProjectOwnerAsync(int projectId);
    
    Task<bool> IsCurrentUserIsProjectExecutorAsync(int projectId);
}