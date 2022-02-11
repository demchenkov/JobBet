using FluentValidation;

namespace JobBet.Application.ProjectAuctions.Queries.GetProjectAuctionsWithPagination;

public class GetProjectAuctionsQueryValidator : AbstractValidator<GetProjectAuctionsQuery>
{
    public GetProjectAuctionsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.")
            .LessThanOrEqualTo(100).WithMessage("PageSize at most less than or equal to 100.");
    }
}