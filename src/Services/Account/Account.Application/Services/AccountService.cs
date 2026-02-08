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
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<AccountResponseDto> CreateAsync(CreateAccountRequestDto createAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateAccountCommand>(createAccountRequestDto);
            command.UserId = principal.GetUserId();
            return await _mediator.Send(command);
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = new DeleteAccountCommand { Id = id, UserId = principal.GetUserId() };
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<AccountResponseDto>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllAccountsQuery { Limit = limit, Offset = offset });
        }

        public async Task<AccountResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAccountByIdQuery { Id = id });

        }

        public async Task UpdateAsync(UpdateAccountRequestDto updateAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateAccountCommand>(updateAccountRequestDto);
            command.UserId = principal.GetUserId();
            await _mediator.Send(command);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAccountExistsQuery { Id = id });
        }

    }
}
