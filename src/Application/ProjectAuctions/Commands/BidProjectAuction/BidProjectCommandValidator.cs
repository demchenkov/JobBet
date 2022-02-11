using FluentValidation;
using JobBet.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.ProjectAuctions.Commands.BidProjectAuction;

public class BidProjectCommandValidator: AbstractValidator<BidProjectCommand>
{
    private readonly IApplicationDbContext _context;
    
    public BidProjectCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .MustAsync(ExistAuctionAndBeActive).WithMessage("Project not exist or auction has been ended");
    }

    public async Task<bool> ExistAuctionAndBeActive(int? projectId, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(x => x.Auction)
            .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken);

        return project?.Auction != null && project.Auction.FinishTime > DateTimeOffset.UtcNow;
    }
}