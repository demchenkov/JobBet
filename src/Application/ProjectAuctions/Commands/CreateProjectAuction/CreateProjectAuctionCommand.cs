using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.ProjectAuctions.Commands.CreateProjectAuction;

public class CreateProjectAuctionCommand : IRequest
{
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? FinishTime { get; set; }
    public int? ProjectId { get; set; }
    public decimal? InitialPrice { get; set; }
}

public class CreateProjectAuctionCommandHandler : IRequestHandler<CreateProjectAuctionCommand>
{
    private readonly IApplicationDbContext _context;
    
    public CreateProjectAuctionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateProjectAuctionCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectAuction
        {
            StartTime = request.StartTime!.Value,
            FinishTime = request.FinishTime!.Value,
            LotId = request.ProjectId!.Value,
            InitialPrice = request.InitialPrice!.Value,
        };

        _context.ProjectAuctions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}