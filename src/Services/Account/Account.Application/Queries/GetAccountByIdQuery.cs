using Account.Application.DTOs;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Queries
{
    public class GetAccountByIdQuery : IQuery<AccountResponseDto>
    {
        public Guid Id { get; set; }
    }
}
