using RefactorThis.Domain.Common.Enums;
using RefactorThis.Persistence;

namespace RefactorThis.Application.Processors.PaymentTypes
{
    public interface IPaymentProcessor
    {
        string ProcessPayment(Invoice invoice, Payment payment);
    }

    public abstract class PaymentProcessor : IPaymentProcessor
    {
        public abstract string ProcessPayment(Invoice invoice, Payment payment);

        protected void UpdateInvoice(Invoice invoice, Payment payment)
        {
            invoice.AmountPaid += payment.Amount;
            if (invoice.Type == InvoiceType.Commercial)
            {
                invoice.TaxAmount += payment.Amount * 0.14m;
            }
            invoice.Payments.Add(payment);
        }
    }
}
