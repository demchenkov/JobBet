using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Common.Services;

public class JobService : IJobService
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public JobService(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> IsUserIsJobOwnerAsync(int jobId, string userId)
    {
        _ = userId ?? throw new ArgumentNullException(nameof(userId));

        Job? job = await _context.Jobs
            .Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == jobId);

        return job?.Client.UserId == userId;
    }

    public async Task<bool> IsUserIsJobExecutorAsync(int jobId, string userId)
    {
        _ = userId ?? throw new ArgumentNullException(nameof(userId));

        Job? job = await _context.Jobs
            .Include(x => x.Executor)
            .FirstOrDefaultAsync(x => x.Id == jobId);

        return job?.Executor?.UserId == userId;
    }

    public Task<bool> IsCurrentUserIsJobOwnerAsync(int jobId)
    {
        return IsUserIsJobOwnerAsync(jobId, _currentUserService.UserId!);
    }

    public Task<bool> IsCurrentUserIsJobExecutorAsync(int jobId)
    {
        return IsUserIsJobExecutorAsync(jobId, _currentUserService.UserId!);
    }
}