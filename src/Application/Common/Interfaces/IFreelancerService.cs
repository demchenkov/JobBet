using JobBet.Domain.Entities;

namespace JobBet.Application.Common.Interfaces;

public interface IFreelancerService
{
    Task<Freelancer?> GetFreelancerByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    Task<Freelancer> GetCurrentUserFreelancerAsync(CancellationToken cancellationToken = default);
}