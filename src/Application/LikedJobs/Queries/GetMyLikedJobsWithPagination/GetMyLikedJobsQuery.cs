using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Mappings;
using JobBet.Application.Common.Models;
using JobBet.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.LikedJobs.Queries.GetMyLikedJobsWithPagination;

public class GetMyLikedJobsQuery : IRequest<PaginatedList<LikedJobDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetMyLikedJobsQueryHandler : IRequestHandler<GetMyLikedJobsQuery, PaginatedList<LikedJobDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IFreelancerService _freelancerService;
    private readonly IMapper _mapper;

    public GetMyLikedJobsQueryHandler(IApplicationDbContext context, IMapper mapper,
        IFreelancerService freelancerService)
    {
        _context = context;
        _mapper = mapper;
        _freelancerService = freelancerService;
    }

    public async Task<PaginatedList<LikedJobDto>> Handle(GetMyLikedJobsQuery request,
        CancellationToken cancellationToken)
    {
        Freelancer? freelancer = await _freelancerService.GetCurrentUserFreelancerAsync(cancellationToken);
        if (freelancer == null)
        {
            throw new NotFoundException(nameof(Freelancer));
        }

        PaginatedList<LikedJobDto> likedJobList = await _context.LikedJobs
            .Include(x => x.Job)
            .Where(x => x.FreelancerId == freelancer.Id)
            .Select(x => x.Job)
            .ProjectTo<LikedJobDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return likedJobList;
    }
}