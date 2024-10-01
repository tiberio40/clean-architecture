using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    [Serializable]

    public class ExternalAuthorizationException : Exception
    {
        public ExternalAuthorizationException()
        {
        }

        public ExternalAuthorizationException(string? message) : base(message)
        {
        }

        public ExternalAuthorizationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
