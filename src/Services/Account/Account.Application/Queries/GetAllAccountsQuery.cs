

using Account.Application.DTOs;
using FluentValidation;
using Shared.Application.Interfaces;

namespace Account.Application.Queries
{
    public class GetAllAccountsQuery : IQuery<IEnumerable<AccountResponseDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }

    public class GetAllAccountsQueryValidator : AbstractValidator<GetAllAccountsQuery>
    {
        public GetAllAccountsQueryValidator()
        {
            RuleFor(x => x.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(25);

            RuleFor(x => x.Offset)
                .GreaterThanOrEqualTo(0);
        }
    }
}
