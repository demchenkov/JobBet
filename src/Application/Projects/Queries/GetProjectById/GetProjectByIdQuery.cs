using AutoMapper;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ProjectDetailsDto>
{
    public int Id { get; set; }
}

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectDetailsDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }
        
        return _mapper.Map<ProjectDetailsDto>(entity);
    }
}