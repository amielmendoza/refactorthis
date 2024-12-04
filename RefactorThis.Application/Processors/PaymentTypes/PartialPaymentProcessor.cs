using Humanizer;
using RefactorThis.Domain.Common.Constants;
using RefactorThis.Domain.Common.Enums;
using RefactorThis.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorThis.Application.Processors.PaymentTypes
{
    public class PartialPaymentProcessor : PaymentProcessor
    {
        public override string ProcessPayment(Invoice invoice, Payment payment)
        {
            decimal totalPaid = invoice.Payments.Sum(x => x.Amount);
            decimal remainingAmount = invoice.Amount - invoice.AmountPaid;

            if (totalPaid != 0 && invoice.Amount == totalPaid)
            {
                return InvoiceResponseMessages.FullyPaid;
            }
            else if (totalPaid != 0 && payment.Amount > totalPaid)
            {
                return InvoiceResponseMessages.PaymentIsGreaterThanPartialAmountRemaining;
            }

            UpdateInvoice(invoice, payment);

            if (remainingAmount == payment.Amount)
            {
                return InvoiceResponseMessages.FinalPartialPaymentIsPaid;
            }
            else
            {
                return InvoiceResponseMessages.AnotherPartialPaymentReceived;
            }
        }
    }
}
