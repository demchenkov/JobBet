using FluentValidation;

namespace JobBet.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
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
        
        RuleFor(x => x.ProjectType)
            .IsInEnum();
        
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
