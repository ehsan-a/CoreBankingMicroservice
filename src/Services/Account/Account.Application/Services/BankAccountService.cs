using Account.Application.Commands;
using Account.Application.DTOs;
using Account.Application.Interfaces;
using Account.Application.Queries;
using AutoMapper;
using MediatR;
using Shared.Application.Extensions;
using System.Security.Claims;

namespace Account.Application.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BankAccountService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BankAccountResponseDto> CreateAsync(CreateBankAccountRequestDto dto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateBankAccountCommand>(dto);
            command.UserId = principal.GetUserId();
            return await _mediator.Send(command);
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = new DeleteBankAccountCommand { Id = id, UserId = principal.GetUserId() };
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<BankAccountResponseDto>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllBankAccountsQuery { Limit = limit, Offset = offset });
        }

        public async Task<BankAccountResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBankAccountByIdQuery { Id = id });

        }

        public async Task UpdateAsync(UpdateBankAccountRequestDto dto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateBankAccountCommand>(dto);
            command.UserId = principal.GetUserId();
            await _mediator.Send(command);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetBankAccountExistsQuery { Id = id });
        }

    }
}
