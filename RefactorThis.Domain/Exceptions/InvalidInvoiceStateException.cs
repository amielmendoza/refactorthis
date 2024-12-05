using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorThis.Domain.Exceptions
{
    public class InvalidInvoiceStateException : Exception
    {
        public InvalidInvoiceStateException(string message) : base(message)
        {
        }
        public InvalidInvoiceStateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
