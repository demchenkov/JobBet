using FluentValidation;

namespace JobBet.Application.Jobs.Commands.CreateJob;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(5)
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(10)
            .MaximumLength(2000);

        RuleFor(x => x.ExperienceLevel)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.JobType)
            .NotNull()
            .IsInEnum();
    }
}