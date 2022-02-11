using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Mappings;
using JobBet.Application.Common.Models;
using MediatR;

namespace JobBet.Application.JobAuctions.Queries.GetJobAuctionsWithPagination;

public class GetJobAuctionsQuery : IRequest<PaginatedList<JobAuctionDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetJobAuctionQueryHandler : IRequestHandler<GetJobAuctionsQuery, PaginatedList<JobAuctionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetJobAuctionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<JobAuctionDto>> Handle(GetJobAuctionsQuery request, CancellationToken cancellationToken)
    {
        return _context.JobAuctions
            .ProjectTo<JobAuctionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}