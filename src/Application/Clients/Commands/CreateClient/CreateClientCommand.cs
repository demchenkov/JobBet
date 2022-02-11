using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.Clients.Commands.CreateClient;

public class CreateClientCommand : IRequest
{
    public string? Title { get; set; }
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateClientCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Client entity = new Client {Title = request.Title!, UserId = _currentUserService.UserId!};

        _context.Clients.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}