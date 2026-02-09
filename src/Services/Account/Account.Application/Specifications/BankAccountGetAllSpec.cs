using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Specifications
{
    public class BankAccountGetAllSpec : BankAccountBaseSpec
    {
        public BankAccountGetAllSpec(int? limit = null, int? offset = null)
        {
            if (limit is not null && offset is not null)
            {
                ApplyPaging(offset.Value, limit.Value);
            }
        }
    }
}
