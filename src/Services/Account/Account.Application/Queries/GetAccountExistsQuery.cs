

using Shared.Application.Interfaces;

namespace Account.Application.Queries
{
    public class GetAccountExistsQuery : IQuery<bool>
    {
        public Guid Id { get; set; }
    }
}

