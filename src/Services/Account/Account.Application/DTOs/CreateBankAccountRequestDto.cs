using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.DTOs
{
    public class CreateBankAccountRequestDto
    {
        public Guid CustomerId { get; set; }
    }
}
