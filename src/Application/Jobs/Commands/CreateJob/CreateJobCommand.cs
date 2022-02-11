using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;
using MediatR;

namespace JobBet.Application.Jobs.Commands.CreateJob;

public class CreateJobCommand : IRequest<int>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public JobType? JobType { get; set; }
    public ExperienceLevel? ExperienceLevel { get; set; }
}

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateJobCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var entity = new Job
        {
            Title = request.Title!,
            Description = request.Description,
            ExperienceLevel = request.ExperienceLevel!.Value,
            JobType = request.JobType,
            Status = JobStatus.Created,
        };
        
        _context.Jobs.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
} 