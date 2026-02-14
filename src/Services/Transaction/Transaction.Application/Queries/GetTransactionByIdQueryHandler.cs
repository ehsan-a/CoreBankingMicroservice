using AutoMapper;
using Shared.Application.Interfaces;
using Transaction.Application.DTOs;
using Transaction.Application.Specifications;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Application.Queries
{
    public class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, TransactionResponseDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            var transaction = await _transactionRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<TransactionResponseDto>(transaction);
        }
    }
}
