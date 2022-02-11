using AutoMapper;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.ProjectAuctions.Queries.GetProjectAuctionById;

public class GetProjectAuctionByIdQuery : IRequest<ProjectAuctionDetailsDto>
{
    public int Id { get; set; }
}

public class GetProjectAuctionByIdQueryHandler : IRequestHandler<GetProjectAuctionByIdQuery, ProjectAuctionDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectAuctionByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectAuctionDetailsDto> Handle(GetProjectAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectAuctions
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException(nameof(ProjectAuction), request.Id);
        }
        
        return _mapper.Map<ProjectAuctionDetailsDto>(entity);
    }
}