using FluentValidation;
namespace JobBet.Application.Freelancers.CreateFreelancer;

public class CreateFreelancerCommandValidator : AbstractValidator<CreateFreelancerCommand>
{
    public CreateFreelancerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(3);
        
        RuleFor(x => x.LastName)
            .MinimumLength(3);
        
        RuleFor(x => x.Title)
            .MinimumLength(5);
        
        RuleFor(x => x.Description)
            .MinimumLength(20);
    }
}
