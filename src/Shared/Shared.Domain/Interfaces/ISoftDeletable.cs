using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
