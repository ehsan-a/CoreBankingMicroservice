using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.Application.DTOs
{
    public class CreateComplianceRequestValidator : AbstractValidator<CreateComplianceRequest>
    {
        public CreateComplianceRequestValidator()
        {
            RuleFor(x => x.NationalCode)
       .NotEmpty()
       .MinimumLength(10);

        }
    }
}
