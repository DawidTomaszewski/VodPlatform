using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.ValueObjects
{
    public class InvalidTitleException : Exception
    {
        public InvalidTitleException(string title)
            : base($"Title '{title}' is invalid. It cannot be null, empty, or whitespace.") { }
    }
}
