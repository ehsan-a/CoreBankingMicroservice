using FluentValidation;

namespace Transaction.Application.DTOs
{
    public class CreateTransactionRequestDtoValidator : AbstractValidator<CreateTransactionRequestDto>
    {
        public CreateTransactionRequestDtoValidator()
        {
            RuleFor(x => x.DebitAccountId)
       .NotEmpty();

            RuleFor(x => x.CreditAccountId)
       .NotEmpty();

        }
    }
}
