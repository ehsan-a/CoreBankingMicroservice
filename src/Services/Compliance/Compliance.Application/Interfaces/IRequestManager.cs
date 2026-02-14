using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.Application.Interfaces
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid id);

        Task CreateRequestForCommandAsync<T>(Guid id);
    }
}
