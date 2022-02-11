using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Common.Services;

public class ClientService : IClientService
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ClientService(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public Task<Client?> GetClientByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return _context.Clients.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public Task<Client?> GetCurrentUserClientAsync(CancellationToken cancellationToken = default)
    {
        string? userId = _currentUserService.UserId;

        if (userId == null)
        {
            throw new Exception("Cannot find current user id");
        }

        return GetClientByUserIdAsync(userId, cancellationToken)!;
    }
}