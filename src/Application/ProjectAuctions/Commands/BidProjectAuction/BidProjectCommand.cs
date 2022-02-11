using JobBet.Application.Common.Interfaces;
using MediatR;

namespace JobBet.Application.ProjectAuctions.Commands.BidProjectAuction;

public class BidProjectCommand : IRequest
{
    public int? Id { get; set; }
    public decimal? Price { get; set; }
}

public class BidProjectCommandHandler : IRequestHandler<BidProjectCommand>
{
    private readonly IBettingService _bettingService;
    private readonly IFreelancerService _freelancerService;

    public BidProjectCommandHandler(IBettingService bettingService, IFreelancerService freelancerService)
    {
        _bettingService = bettingService;
        _freelancerService = freelancerService;
    }

    public async Task<Unit> Handle(BidProjectCommand request, CancellationToken cancellationToken)
    {
        var freelancer = await _freelancerService.GetCurrentUserFreelancerAsync(cancellationToken);
        
        await _bettingService.MakeBetAsync(freelancer!.Id, request.Id!.Value, request.Price!.Value);
        
        return Unit.Value;
    }
}

