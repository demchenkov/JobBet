using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;
using MediatR;

namespace JobBet.Application.Jobs.Commands.UpdateJob;

public class UpdateJobCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public JobType? JobType { get; set; }
    public ExperienceLevel? ExperienceLevel { get; set; }
    public JobStatus? Status { get; set; }
}

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand>
{
    private readonly IApplicationDbContext _context;


    public UpdateJobCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Jobs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id);
        }

        entity.Title = request.Title ?? entity.Title;
        entity.Description = request.Description ?? entity.Description;
        entity.JobType = request.JobType ?? entity.JobType;
        entity.ExperienceLevel = request.ExperienceLevel ?? entity.ExperienceLevel;
        entity.Status = request.Status ?? entity.Status;
        

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}