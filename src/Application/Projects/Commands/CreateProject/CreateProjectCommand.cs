using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;
using MediatR;

namespace JobBet.Application.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<int>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ProjectType? ProjectType { get; set; }
    public ExperienceLevel? ExperienceLevel { get; set; }
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = new Project
        {
            Title = request.Title!,
            Description = request.Description,
            ExperienceLevel = request.ExperienceLevel!.Value,
            ProjectType = request.ProjectType,
            Status = ProjectStatus.Created,
        };
        
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
} 