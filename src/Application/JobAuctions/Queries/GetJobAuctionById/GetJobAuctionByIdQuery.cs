using AutoMapper;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.JobAuctions.Queries.GetJobAuctionById;

public class GetJobAuctionByIdQuery : IRequest<JobAuctionDetailsDto>
{
    public int Id { get; set; }
}

public class GetJobAuctionByIdQueryHandler : IRequestHandler<GetJobAuctionByIdQuery, JobAuctionDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetJobAuctionByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<JobAuctionDetailsDto> Handle(GetJobAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.JobAuctions
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException(nameof(JobAuction), request.Id);
        }
        
        return _mapper.Map<JobAuctionDetailsDto>(entity);
    }
}