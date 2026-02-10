using FluentValidation;

namespace Identity.API.Application.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
