using AutoMapper;
using Shared.Application.Interfaces;
using Transaction.Application.DTOs;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Application.Commands
{
    public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, TransactionResponseDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {

            if (request.DebitAccountId == request.CreditAccountId)
                throw new Shared.Application.Exceptions.ConflictException("The parties to the transaction are the same.");

            var transaction = AccountTransaction.Create(
                request.DebitAccountId,
                request.CreditAccountId,
                request.Amount,
                request.Description,
                request.Type,
                request.UserId);

            await _transactionRepository.AddAsync(transaction, cancellationToken);

            await _transactionRepository.UnitOfWork.SaveEntitiesAsync();
            return _mapper.Map<TransactionResponseDto>(transaction);
        }
    }
}
