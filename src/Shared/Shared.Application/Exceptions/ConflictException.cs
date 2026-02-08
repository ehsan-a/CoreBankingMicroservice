using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Exceptions
{
    public sealed class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message)
        {
        }
    }
}
