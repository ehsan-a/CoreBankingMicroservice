using Shared.Application.Interfaces;
using Transaction.Application.DTOs;

namespace Transaction.Application.Queries
{
    public class GetAllTransactionsQuery : IQuery<IEnumerable<TransactionResponseDto>>
    {
    }
}
