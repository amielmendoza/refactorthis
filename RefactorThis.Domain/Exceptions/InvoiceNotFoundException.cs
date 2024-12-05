using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorThis.Domain.Exceptions
{
    public class InvoiceNotFoundException : Exception
    {
        public InvoiceNotFoundException(string message) : base(message)
        {
        }
        public InvoiceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
