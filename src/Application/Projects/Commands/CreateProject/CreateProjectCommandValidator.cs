using FluentValidation;

namespace JobBet.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
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
        
        RuleFor(x => x.ProjectType)
            .NotNull()
            .IsInEnum();
    }
}