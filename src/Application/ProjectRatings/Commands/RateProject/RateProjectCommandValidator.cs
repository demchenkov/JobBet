using FluentValidation;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.ProjectRatings.Commands.RateProject;

public class RateProjectCommandValidator : AbstractValidator<RateProjectCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IProjectService _projectService;
    
    public RateProjectCommandValidator(IApplicationDbContext context, IProjectService projectService)
    {
        _context = context;
        _projectService = projectService;

        RuleFor(x => x.ProjectId)
            .NotNull()
            .MustAsync(ProjectExist)
            .MustAsync(BeOwnerOrExecutor).WithMessage("To rate this project current user must be owner or executor");
    }

    public async Task<bool> ProjectExist(int? projectId, CancellationToken cancellationToken)
    {
        var exist = await _context.Projects.AnyAsync(x => x.Id == projectId, cancellationToken);
        if (!exist)
        {
            throw new NotFoundException(nameof(Project), projectId!);
        }

        return true;
    }
    
    public async Task<bool> BeOwnerOrExecutor(int? projectId, CancellationToken cancellationToken)
    {
        return await _projectService.IsCurrentUserIsProjectOwnerAsync(projectId!.Value) ||
               await _projectService.IsCurrentUserIsProjectExecutorAsync(projectId!.Value);
    }
}