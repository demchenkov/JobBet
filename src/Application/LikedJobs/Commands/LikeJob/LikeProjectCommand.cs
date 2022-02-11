using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.LikedJobs.Commands.LikeJob;

public class LikeJobCommand : IRequest
{
    public int? JobId { get; set; }
}

public class LikeJobCommandHandler : IRequestHandler<LikeJobCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IFreelancerService _freelancerService;

    public LikeJobCommandHandler(IApplicationDbContext context, IFreelancerService freelancerService)
    {
        _context = context;
        _freelancerService = freelancerService;
    }

    public async Task<Unit> Handle(LikeJobCommand request, CancellationToken cancellationToken)
    {
        Job? job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId, cancellationToken);
        Freelancer? freelancer = await _freelancerService.GetCurrentUserFreelancerAsync(cancellationToken);

        if (job == null || freelancer == null)
        {
            throw new NotFoundException(nameof(Job), request.JobId);
        }

        bool isJobLiked = await _context.LikedJobs
            .AnyAsync(x => x.FreelancerId == freelancer.Id && x.JobId == job.Id, cancellationToken);

        if (isJobLiked)
        {
            return Unit.Value;
        }

        LikedJob entity = new LikedJob {Job = job, Freelancer = freelancer};

        _context.LikedJobs.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}