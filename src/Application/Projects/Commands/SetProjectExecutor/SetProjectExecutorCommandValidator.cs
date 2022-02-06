using FluentValidation;
using JobBet.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Projects.Commands.SetProjectExecutor;

public class SetProjectExecutorCommandValidator : AbstractValidator<SetProjectExecutorCommand>
{
    private readonly IApplicationDbContext _context;
    
    public SetProjectExecutorCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.ExecutorId)
            .NotEmpty()
            .MustAsync(ExecutorExist).WithMessage("The specified executor not exist");
    }
    
    public Task<bool> ExecutorExist(int? executorId, CancellationToken cancellationToken)
    {
        return _context.Freelancers.AnyAsync(x => x.Id == executorId, cancellationToken);
    } 
}
