using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Interfaces
{
    public interface INumberGenerator
    {
        Task<string> GenerateAccountNumberAsync();
    }
}
