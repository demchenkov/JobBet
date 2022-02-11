using FluentValidation;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.JobRatings.Commands.RateJob;

public class RateJobCommandValidator : AbstractValidator<RateJobCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IJobService _projectService;
    
    public RateJobCommandValidator(IApplicationDbContext context, IJobService projectService)
    {
        _context = context;
        _projectService = projectService;

        RuleFor(x => x.JobId)
            .NotNull()
            .MustAsync(JobExist)
            .MustAsync(BeOwnerOrExecutor).WithMessage("To rate this project current user must be owner or executor");
    }

    public async Task<bool> JobExist(int? jobId, CancellationToken cancellationToken)
    {
        var exist = await _context.Jobs.AnyAsync(x => x.Id == jobId, cancellationToken);
        if (!exist)
        {
            throw new NotFoundException(nameof(Job), jobId!);
        }

        return true;
    }
    
    public async Task<bool> BeOwnerOrExecutor(int? jobId, CancellationToken cancellationToken)
    {
        return await _projectService.IsCurrentUserIsJobOwnerAsync(jobId!.Value) ||
               await _projectService.IsCurrentUserIsJobExecutorAsync(jobId!.Value);
    }
}