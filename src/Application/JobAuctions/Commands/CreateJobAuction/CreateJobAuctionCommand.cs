using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.JobAuctions.Commands.CreateJobAuction;

public class CreateJobAuctionCommand : IRequest
{
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? FinishTime { get; set; }
    public int? JobId { get; set; }
    public decimal? InitialPrice { get; set; }
}

public class CreateJobAuctionCommandHandler : IRequestHandler<CreateJobAuctionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateJobAuctionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateJobAuctionCommand request, CancellationToken cancellationToken)
    {
        JobAuction entity = new JobAuction
        {
            StartTime = request.StartTime!.Value,
            FinishTime = request.FinishTime!.Value,
            LotId = request.JobId!.Value,
            InitialPrice = request.InitialPrice!.Value
        };

        _context.JobAuctions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}