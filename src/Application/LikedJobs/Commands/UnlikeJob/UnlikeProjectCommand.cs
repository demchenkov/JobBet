using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.LikedJobs.Commands.UnlikeJob;

public class UnlikeJobCommand : IRequest
{
    public int? JobId { get; set; }
}

public class UnlikeJobCommandHandler : IRequestHandler<UnlikeJobCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IFreelancerService _freelancerService;

    public UnlikeJobCommandHandler(IApplicationDbContext context, IFreelancerService freelancerService)
    {
        _context = context;
        _freelancerService = freelancerService;
    }

    public async Task<Unit> Handle(UnlikeJobCommand request, CancellationToken cancellationToken)
    {
        Job? job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId, cancellationToken);
        Freelancer? freelancer = await _freelancerService.GetCurrentUserFreelancerAsync(cancellationToken);

        if (job == null || freelancer == null)
        {
            throw new NotFoundException(nameof(Job), request.JobId);
        }

        LikedJob? likedJob = await _context.LikedJobs
            .FirstOrDefaultAsync(x => x.FreelancerId == freelancer.Id && x.JobId == job.Id, cancellationToken);

        if (likedJob == null)
        {
            return Unit.Value;
        }

        _context.LikedJobs.Remove(likedJob);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}