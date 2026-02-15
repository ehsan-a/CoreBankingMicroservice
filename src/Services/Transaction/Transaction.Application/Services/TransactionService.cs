using AutoMapper;
using MediatR;
using Shared.ServiceDefaults;
using System.Security.Claims;
using Transaction.Application.Commands;
using Transaction.Application.DTOs;
using Transaction.Application.Interfaces;
using Transaction.Application.Queries;

namespace Transaction.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TransactionService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TransactionResponseDto> CreateAsync(CreateTransactionRequestDto createTransactionRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateTransactionCommand>(createTransactionRequestDto);
            command.UserId = principal.GetUserId();
            return await _mediator.Send(command);
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllTransactionsQuery());
        }

        public async Task<TransactionResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTransactionByIdQuery { Id = id });
        }
    }
}
