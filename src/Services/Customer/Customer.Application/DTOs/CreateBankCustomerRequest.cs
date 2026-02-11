using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.DTOs
{
    public class CreateBankCustomerRequest
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
