using FluentValidation;
using JobBet.Application.Common.Exceptions;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.JobAuctions.Commands.CreateJobAuction;

public class CreateJobAuctionCommandValidator : AbstractValidator<CreateJobAuctionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateJobAuctionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.StartTime)
            .NotNull()
            .Must(BeGreaterThanNowWithAccuracy(2)).WithMessage("StartTime cannot be in the past");

        RuleFor(x => x.FinishTime)
            .NotNull()
            .Must(DurationBeGreaterThan(1)).WithMessage($"Minimal duration is {1} hour(s)");

        RuleFor(x => x.JobId)
            .NotNull()
            .MustAsync(JobExist);

        RuleFor(x => x.InitialPrice)
            .NotNull()
            .GreaterThan(0);
    }

    private static Func<DateTimeOffset?, bool> BeGreaterThanNowWithAccuracy(int minutes)
    {
        return startTime => DateTimeOffset.UtcNow.AddMinutes(-minutes) < startTime!.Value.ToUniversalTime();
    }

    private static Func<CreateJobAuctionCommand, DateTimeOffset?, bool> DurationBeGreaterThan(int hours)
    {
        return (command, _) => (command.FinishTime!.Value - command.StartTime!.Value).Hours > hours;
    }

    private async Task<bool> JobExist(int? jobId, CancellationToken cancellationToken)
    {
        bool exist = await _context.Jobs
            .AnyAsync(x => x.Id == jobId!.Value, cancellationToken);
        if (!exist)
        {
            throw new NotFoundException(nameof(Job), jobId!);
        }

        return true;
    }
}