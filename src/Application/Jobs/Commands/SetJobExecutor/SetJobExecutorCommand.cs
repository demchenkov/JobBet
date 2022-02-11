using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;
using MediatR;

namespace JobBet.Application.Jobs.Commands.SetJobExecutor;

public class SetJobExecutorCommand : IRequest
{
    public int? Id { get; set; }
    public int? ExecutorId { get; set; }
}

public class SetJobExecutorCommandHandler : IRequestHandler<SetJobExecutorCommand>
{
    private readonly IApplicationDbContext _context;

    public SetJobExecutorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SetJobExecutorCommand request, CancellationToken cancellationToken)
    {
        Job? entity = await _context.Jobs
            .FindAsync(new object[] {request.Id!}, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id!);
        }

        entity.ExecutorId = request.ExecutorId!.Value;
        entity.Status = JobStatus.InProgress;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}