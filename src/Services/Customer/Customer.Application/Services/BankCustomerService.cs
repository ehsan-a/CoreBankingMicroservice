using AutoMapper;
using Customer.Application.Commands;
using Customer.Application.DTOs;
using Customer.Application.Interfaces;
using Customer.Application.Queries;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using MediatR;
using Shared.ServiceDefaults;
using System.Security.Claims;

namespace Customer.Application.Services
{
    public class BankCustomerService : IBankCustomerService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BankCustomerService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BankCustomerResponseDto> CreateAsync(CreateBankCustomerRequest request, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateBankCustomerCommand>(request);
            command.UserId = principal.GetUserId();
            return await _mediator.Send(command);
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = new DeleteBankCustomerCommand { Id = id, UserId = principal.GetUserId() };
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<BankCustomerResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllBankCustomersQuery());
        }

        public async Task<BankCustomerResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBankCustomerByIdQuery { Id = id });
        }

        public async Task<BankCustomer?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBankCustomerByNationalCodeQuery { NationalCode = nationalCode });
        }

        public async Task UpdateAsync(UpdateBankCustomerRequest request, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateBankCustomerCommand>(request);
            command.UserId = principal.GetUserId();
            await _mediator.Send(command);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBankCustomerExistsQuery { Id = id });
        }
    }
}
