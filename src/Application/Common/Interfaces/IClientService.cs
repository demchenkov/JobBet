using JobBet.Domain.Entities;

namespace JobBet.Application.Common.Interfaces;

public interface IClientService
{
    Task<Client?> GetClientByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    Task<Client?> GetCurrentUserClientAsync(CancellationToken cancellationToken = default);
}