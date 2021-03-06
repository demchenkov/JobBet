using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommand : IRequest
{
    public string? Title { get; set; }
}

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateClientCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        Client entity = new Client {Title = request.Title!, UserId = _currentUserService.UserId!};

        _context.Clients.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}