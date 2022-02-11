using FluentValidation;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Jobs.Commands.SetJobExecutor;

public class SetJobExecutorCommandValidator : AbstractValidator<SetJobExecutorCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IClientService _clientService;
    
    public SetJobExecutorCommandValidator(IApplicationDbContext context, IClientService clientService)
    {
        _context = context;
        _clientService = clientService;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(BeOwner);

        RuleFor(x => x.ExecutorId)
            .NotEmpty()
            .MustAsync(ExecutorExist).WithMessage("The specified executor not exist");
    }

    public async Task<bool> BeOwner(SetJobExecutorCommand command, int? id, CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        var currentClient = await _clientService.GetCurrentUserClientAsync(cancellationToken);
        
        return currentClient!.Id == job?.ClientId;
    }
    
    public Task<bool> ExecutorExist(int? executorId, CancellationToken cancellationToken)
    {
        return _context.Freelancers.AnyAsync(x => x.Id == executorId, cancellationToken);
    } 
}
