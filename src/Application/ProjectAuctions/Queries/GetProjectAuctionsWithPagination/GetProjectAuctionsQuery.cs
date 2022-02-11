using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Mappings;
using JobBet.Application.Common.Models;
using MediatR;

namespace JobBet.Application.ProjectAuctions.Queries.GetProjectAuctionsWithPagination;

public class GetProjectAuctionsQuery : IRequest<PaginatedList<ProjectAuctionDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProjectAuctionQueryHandler : IRequestHandler<GetProjectAuctionsQuery, PaginatedList<ProjectAuctionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectAuctionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<ProjectAuctionDto>> Handle(GetProjectAuctionsQuery request, CancellationToken cancellationToken)
    {
        return _context.ProjectAuctions
            .ProjectTo<ProjectAuctionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}