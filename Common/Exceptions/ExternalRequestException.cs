using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    [Serializable]
    public class ExternalRequestException : Exception
    {
        public ExternalRequestException()
        {
        }

        public ExternalRequestException(string? message) : base(message)
        {
        }

        public ExternalRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
