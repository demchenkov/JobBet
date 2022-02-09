using FluentValidation;

namespace JobBet.Application.Clients.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(5);
    }
}