using FluentValidation;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Projects.Commands.SetProjectExecutor;

public class SetProjectExecutorCommandValidator : AbstractValidator<SetProjectExecutorCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IClientService _clientService;
    
    public SetProjectExecutorCommandValidator(IApplicationDbContext context, IClientService clientService)
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

    public async Task<bool> BeOwner(SetProjectExecutorCommand command, int? id, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        var currentClient = await _clientService.GetCurrentUserClientAsync(cancellationToken);
        
        return currentClient!.Id == project?.ClientId;
    }
    
    public Task<bool> ExecutorExist(int? executorId, CancellationToken cancellationToken)
    {
        return _context.Freelancers.AnyAsync(x => x.Id == executorId, cancellationToken);
    } 
}
