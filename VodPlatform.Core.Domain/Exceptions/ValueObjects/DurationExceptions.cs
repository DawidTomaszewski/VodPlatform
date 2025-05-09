using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.ValueObjects
{
    public class InvalidDurationException : Exception
    {
        public InvalidDurationException(int totalSeconds)
            : base($"Duration value '{totalSeconds}' is invalid. It must be greater than or equal to 0.") { }
    }
}
