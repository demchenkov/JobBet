using AutoMapper;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.Jobs.Queries.GetJobById;

public class GetJobByIdQuery : IRequest<JobDetailsDto>
{
    public int Id { get; set; }
}

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetJobByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<JobDetailsDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        Job? entity = await _context.Jobs
            .FindAsync(new object[] {request.Id}, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id);
        }

        return _mapper.Map<JobDetailsDto>(entity);
    }
}