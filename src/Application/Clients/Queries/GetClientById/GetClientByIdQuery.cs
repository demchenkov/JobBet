using AutoMapper;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Clients.Queries.GetClientById;

public class GetClientByIdQuery : IRequest<ClientDetailsDto>
{
    public int Id { get; set; }
}

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetClientByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClientDetailsDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        Client? entity = await _context.Clients
            .Include(x => x.Rating)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Client), request.Id);
        }

        return _mapper.Map<ClientDetailsDto>(entity);
    }
}