using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.Projects.Commands.BetProject;

public class BetProjectCommand : IRequest
{
    public int? Id { get; set; }
    public decimal? Price { get; set; }
}

public class BetProjectCommandHandler : IRequestHandler<BetProjectCommand>
{
    private readonly IBettingService _bettingService;
    private readonly IFreelancerService _freelancerService;

    public BetProjectCommandHandler(IBettingService bettingService, IFreelancerService freelancerService)
    {
        _bettingService = bettingService;
        _freelancerService = freelancerService;
    }

    public async Task<Unit> Handle(BetProjectCommand request, CancellationToken cancellationToken)
    {
        var freelancer = await _freelancerService.GetCurrentUserFreelancerAsync(cancellationToken);
        
        await _bettingService.MakeBetAsync(freelancer!.Id, request.Id!.Value, request.Price!.Value);
        
        return Unit.Value;
    }
}

