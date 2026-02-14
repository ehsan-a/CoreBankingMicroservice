using AutoMapper;
using Shared.Application.Interfaces;
using Transaction.Application.DTOs;
using Transaction.Application.Specifications;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Application.Queries
{
    public class GetAllTransactionsQueryHandler : IQueryHandler<GetAllTransactionsQuery, IEnumerable<TransactionResponseDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionResponseDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            var transactions = await _transactionRepository.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<TransactionResponseDto>>(transactions);
        }
    }
}
