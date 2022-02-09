using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;
using MediatR;

namespace JobBet.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public ProjectType? ProjectType { get; set; }
    public ExperienceLevel? ExperienceLevel { get; set; }
    public ProjectStatus? Status { get; set; }
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IApplicationDbContext _context;


    public UpdateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.Title = request.Title ?? entity.Title;
        entity.Description = request.Description ?? entity.Description;
        entity.ProjectType = request.ProjectType ?? entity.ProjectType;
        entity.ExperienceLevel = request.ExperienceLevel ?? entity.ExperienceLevel;
        entity.Status = request.Status ?? entity.Status;
        

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}