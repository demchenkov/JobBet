using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.Projects.Commands.SetProjectExecutor;

public class SetProjectExecutorCommand : IRequest
{
    public int? Id { get; set; }
    public int? ExecutorId { get; set; }
}

public class SetProjectExecutorCommandHandler : IRequestHandler<SetProjectExecutorCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProjectExecutorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SetProjectExecutorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .FindAsync(new object[] {request.Id!}, cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id!);
        }

        entity.ExecutorId = request.ExecutorId!.Value;
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}