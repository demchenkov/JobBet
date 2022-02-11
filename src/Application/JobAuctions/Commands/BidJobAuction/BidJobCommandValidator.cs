using FluentValidation;
using JobBet.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.JobAuctions.Commands.BidJobAuction;

public class BidJobCommandValidator: AbstractValidator<BidJobCommand>
{
    private readonly IApplicationDbContext _context;
    
    public BidJobCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .MustAsync(ExistAuctionAndBeActive).WithMessage("Job not exist or auction has been ended");
    }

    public async Task<bool> ExistAuctionAndBeActive(int? jobId, CancellationToken cancellationToken)
    {
        var job = await _context.Jobs
            .Include(x => x.Auction)
            .FirstOrDefaultAsync(x => x.Id == jobId, cancellationToken);

        return job?.Auction != null && job.Auction.FinishTime > DateTimeOffset.UtcNow;
    }
}