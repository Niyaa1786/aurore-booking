using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
