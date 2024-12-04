using Humanizer;
using RefactorThis.Domain.Common.Constants;
using RefactorThis.Persistence;

namespace RefactorThis.Application.Processors.PaymentTypes
{
    public class FullPaymentProcessor : PaymentProcessor
    {
        public override string ProcessPayment(Invoice invoice, Payment payment)
        {
            if (payment.Amount > invoice.Amount)
            {
                return InvoiceResponseMessages.PaymentIsGreaterThanInvoiceAmount;
            }
            else if (invoice.Amount == payment.Amount)
            {
                UpdateInvoice(invoice, payment);
                return InvoiceResponseMessages.InvoiceIsNowFullyPaid;
            }
            else
            {
                UpdateInvoice(invoice, payment);
                return InvoiceResponseMessages.InvoiceIsNowPartiallyPaid;
            }
        }
    }
}
