using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string entity, object key) : base($"{entity} with ID '{key}' was not found.") { }
    }
}
