using Account.Domain.Aggregates.BankAccountAggregate;

namespace Account.Application.DTOs
{
    public class BankAccountResponseDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;

        public decimal Balance { get; set; }

        public BankAccountStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CustomerId { get; set; }
    }
}
