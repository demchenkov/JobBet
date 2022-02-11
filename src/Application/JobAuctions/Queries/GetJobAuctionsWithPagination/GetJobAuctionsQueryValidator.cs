using FluentValidation;

namespace JobBet.Application.JobAuctions.Queries.GetJobAuctionsWithPagination;

public class GetJobAuctionsQueryValidator : AbstractValidator<GetJobAuctionsQuery>
{
    public GetJobAuctionsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.")
            .LessThanOrEqualTo(100).WithMessage("PageSize at most less than or equal to 100.");
    }
}