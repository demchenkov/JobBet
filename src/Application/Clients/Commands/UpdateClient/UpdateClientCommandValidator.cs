using FluentValidation;

namespace JobBet.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(5);
    }
}