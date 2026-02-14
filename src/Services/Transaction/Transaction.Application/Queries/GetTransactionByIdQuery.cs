using Shared.Application.Interfaces;
using Transaction.Application.DTOs;

namespace Transaction.Application.Queries
{
    public class GetTransactionByIdQuery : IQuery<TransactionResponseDto>
    {
        public Guid Id { get; set; }
    }
}
