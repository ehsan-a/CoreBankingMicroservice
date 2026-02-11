using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetBankCustomerExistsQuery : IQuery<bool>
    {
        public Guid Id { get; set; }
    }
}
