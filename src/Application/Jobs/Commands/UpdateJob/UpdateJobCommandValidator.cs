using FluentValidation;

namespace JobBet.Application.Jobs.Commands.UpdateJob;

public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Title)
            .MaximumLength(5)
            .MaximumLength(200);
        
        RuleFor(x => x.Description)
            .MaximumLength(10)
            .MaximumLength(2000);

        RuleFor(x => x.ExperienceLevel)
            .IsInEnum();
        
        RuleFor(x => x.JobType)
            .IsInEnum();
        
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
