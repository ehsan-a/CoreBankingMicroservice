using AutoMapper;
using Compliance.Application.Commands;
using Compliance.Application.DTOs;
using Compliance.Application.Interfaces;
using Compliance.Application.Queries;
using FluentValidation;
using MediatR;
using Shared.ServiceDefaults;
using System.Security.Claims;

namespace Compliance.Application.Services
{
    public class ComplianceService : IComplianceService
    {

        private readonly IMapper _mapper;
        private readonly IValidator<CreateComplianceRequest> _validator;
        private readonly IMediator _mediator;

        public ComplianceService(IMapper mapper, IValidator<CreateComplianceRequest> validator, IMediator mediator)
        {
            _mapper = mapper;
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<RegisteredComplianceResponseDto> CreateAsync(CreateComplianceRequest request, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var command = _mapper.Map<CreateComplianceCommand>(request);
            command.UserId = principal.GetUserId();
            return await _mediator.Send(command);
        }

        public async Task<ComplianceResponseDto?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetComplianceByNationalCodeQuery { NationalCode = nationalCode });
        }
        public async Task<bool> ExistsAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetComplianceExistsQuery { NationalCode = nationalCode });
        }
        public async Task<ComplianceResponseDto> GetInquiryAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetInquiryComplianceQuery { NationalCode = nationalCode });
        }
    }
}
