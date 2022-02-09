using JobBet.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Common.Services;

public class ProjectService : IProjectService
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ProjectService(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> IsUserIsProjectOwnerAsync(int projectId, string userId)
    {
        _ = userId ?? throw new ArgumentNullException(nameof(userId));
        
        var project = await _context.Projects
            .Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == projectId);

        return project?.Client.UserId == userId;
    }

    public async Task<bool> IsUserIsProjectExecutorAsync(int projectId, string userId)
    {
        _ = userId ?? throw new ArgumentNullException(nameof(userId));
        
        var project = await _context.Projects
            .Include(x => x.Executor)
            .FirstOrDefaultAsync(x => x.Id == projectId);

        return project?.Executor?.UserId == userId;
    }

    public Task<bool> IsCurrentUserIsProjectOwnerAsync(int projectId)
    {
        return IsUserIsProjectOwnerAsync(projectId, _currentUserService.UserId!);
    }

    public Task<bool> IsCurrentUserIsProjectExecutorAsync(int projectId)
    {
        return IsUserIsProjectExecutorAsync(projectId, _currentUserService.UserId!);
    }
}