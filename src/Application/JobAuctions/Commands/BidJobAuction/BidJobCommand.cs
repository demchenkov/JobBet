using JobBet.Application.Common.Interfaces;
using MediatR;

namespace JobBet.Application.JobAuctions.Commands.BidJobAuction;

public class BidJobCommand : IRequest
{
    public int? Id { get; set; }
    public decimal? Price { get; set; }
}

public class BidJobCommandHandler : IRequestHandler<BidJobCommand>
{
    private readonly IBettingService _bettingService;
    private readonly IFreelancerService _freelancerService;

    public BidJobCommandHandler(IBettingService bettingService, IFreelancerService freelancerService)
    {
        _bettingService = bettingService;
        _freelancerService = freelancerService;
    }

    public async Task<Unit> Handle(BidJobCommand request, CancellationToken cancellationToken)
    {
        var freelancer = await _freelancerService.GetCurrentUserFreelancerAsync(cancellationToken);
        
        await _bettingService.MakeBetAsync(freelancer!.Id, request.Id!.Value, request.Price!.Value);
        
        return Unit.Value;
    }
}

