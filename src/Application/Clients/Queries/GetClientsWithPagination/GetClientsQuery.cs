using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Mappings;
using JobBet.Application.Common.Models;
using MediatR;

namespace JobBet.Application.Clients.Queries.GetClientsWithPagination;

public class GetClientsQuery : IRequest<PaginatedList<ClientDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetClientQueryHandler : IRequestHandler<GetClientsQuery, PaginatedList<ClientDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetClientQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return _context.Clients
            .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}