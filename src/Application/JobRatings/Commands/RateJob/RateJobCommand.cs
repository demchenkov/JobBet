using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.JobRatings.Commands.RateJob;

public class RateJobCommand : IRequest
{
    public int? JobId { get; set; }
    public int? Score { get; set; }
}

public class RateJobCommandHandler : IRequestHandler<RateJobCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IJobService _projectService;

    public RateJobCommandHandler(IApplicationDbContext context, IJobService projectService)
    {
        _context = context;
        _projectService = projectService;
    }

    public async Task<Unit> Handle(RateJobCommand request, CancellationToken cancellationToken)
    {
        int jobId = request.JobId!.Value;
        var entity = await _context.JobRatings
                .FirstAsync(x => x.JobId == jobId, cancellationToken);
        
        if (await _projectService.IsCurrentUserIsJobOwnerAsync(jobId))
        {
            entity.FreelancerScore = request.Score!;
        }
        
        if (await _projectService.IsCurrentUserIsJobExecutorAsync(jobId))
        {
            entity.ClientScore = request.Score!;
        }

        _context.JobRatings.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}