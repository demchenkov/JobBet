using FluentValidation;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.ProjectAuctions.Commands.CreateProjectAuction;

public class CreateProjectAuctionCommandValidator : AbstractValidator<CreateProjectAuctionCommand>
{
    private readonly IApplicationDbContext _context;
    
    public CreateProjectAuctionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.StartTime)
            .NotNull()
            .Must(BeGreaterThanNowWithAccuracy(2)).WithMessage("StartTime cannot be in the past");
        
        RuleFor(x => x.FinishTime)
            .NotNull()
            .Must(DurationBeGreaterThan(1)).WithMessage($"Minimal duration is {1} hour(s)");
        
        RuleFor(x => x.ProjectId)
            .NotNull()
            .MustAsync(ProjectExist);
        
        RuleFor(x => x.InitialPrice)
            .NotNull()
            .GreaterThan(0);
    }

    private static Func<DateTimeOffset?, bool> BeGreaterThanNowWithAccuracy(int minutes)
    {
        return startTime => DateTimeOffset.UtcNow.AddMinutes(-minutes) < startTime!.Value.ToUniversalTime();
    }

    private static Func<CreateProjectAuctionCommand,DateTimeOffset?,bool> DurationBeGreaterThan(int hours)
    {
        return (command, _) => (command.FinishTime!.Value - command.StartTime!.Value).Hours > hours;
    }

    private async Task<bool> ProjectExist(int? projectId, CancellationToken cancellationToken)
    {
        var exist = await _context.Projects
            .AnyAsync(x => x.Id == projectId!.Value, cancellationToken);
        if (!exist)
        {
            throw new NotFoundException(nameof(Project), projectId!);
        }

        return true;
    }
}