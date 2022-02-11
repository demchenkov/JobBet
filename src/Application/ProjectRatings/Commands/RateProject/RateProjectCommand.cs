using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.ProjectRatings.Commands.RateProject;

public class RateProjectCommand : IRequest
{
    public int? ProjectId { get; set; }
    public int? Score { get; set; }
}

public class RateProjectCommandHandler : IRequestHandler<RateProjectCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IProjectService _projectService;

    public RateProjectCommandHandler(IApplicationDbContext context, IProjectService projectService)
    {
        _context = context;
        _projectService = projectService;
    }

    public async Task<Unit> Handle(RateProjectCommand request, CancellationToken cancellationToken)
    {
        int projectId = request.ProjectId!.Value;
        var entity = await _context.ProjectRatings
                .FirstAsync(x => x.ProjectId == projectId, cancellationToken);
        
        if (await _projectService.IsCurrentUserIsProjectOwnerAsync(projectId))
        {
            entity.FreelancerScore = request.Score!;
        }
        
        if (await _projectService.IsCurrentUserIsProjectExecutorAsync(projectId))
        {
            entity.ClientScore = request.Score!;
        }

        _context.ProjectRatings.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}