using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using MediatR;

namespace JobBet.Application.Freelancers.CreateFreelancer;

public class CreateFreelancerCommand : IRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    // TODO: complete model here
}

public class CreateFreelancerCommandHandler : IRequestHandler<CreateFreelancerCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateFreelancerCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Unit> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
    {
        Freelancer entity = new Freelancer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Title = request.Title,
            Description = request.Description,
            AvatarUrl = string.Empty,
            UserId = _currentUserService.UserId!
        };

        // entity.Skills.AddRange(null);
        // entity.LanguageSkills.AddRange(null);


        _context.Freelancers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}