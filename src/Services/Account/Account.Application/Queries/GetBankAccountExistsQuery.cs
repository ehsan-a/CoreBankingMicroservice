using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Queries
{
    public class GetBankAccountExistsQuery : IQuery<bool>
    {
        public Guid Id { get; set; }
    }
}
