using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.DTOs
{
    public class UpdateBankCustomerRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
