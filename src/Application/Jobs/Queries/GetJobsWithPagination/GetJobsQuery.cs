using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Mappings;
using JobBet.Application.Common.Models;
using MediatR;

namespace JobBet.Application.Jobs.Queries.GetJobsWithPagination;

public class GetJobsQuery : IRequest<PaginatedList<JobDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetJobQueryHandler : IRequestHandler<GetJobsQuery, PaginatedList<JobDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetJobQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<JobDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
    {
        return _context.Jobs
            .ProjectTo<JobDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}