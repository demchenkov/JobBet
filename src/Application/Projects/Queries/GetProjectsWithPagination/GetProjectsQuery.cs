using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Mappings;
using JobBet.Application.Common.Models;
using MediatR;

namespace JobBet.Application.Projects.Queries.GetProjectsWithPagination;

public class GetProjectsQuery : IRequest<PaginatedList<ProjectDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetProjectQueryHandler : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return _context.Projects
            .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}