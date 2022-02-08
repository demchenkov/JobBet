using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Common.Services;

public class FreelancerService : IFreelancerService
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public FreelancerService(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public Task<Freelancer?> GetFreelancerByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return _context.Freelancers.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public Task<Freelancer> GetCurrentUserFreelancerAsync(CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
        {
            throw new Exception("Cannot find current user id");
        }
        
        return GetFreelancerByUserIdAsync(userId, cancellationToken)!;
    }
}