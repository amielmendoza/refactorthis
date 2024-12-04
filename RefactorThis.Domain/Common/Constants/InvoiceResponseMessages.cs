using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorThis.Domain.Common.Constants
{
    public class InvoiceResponseMessages
    {
        public static string NoPaymentNeeded = "No payment needed";
        public static string FullyPaid = "Invoice was already fully paid";
        public static string PaymentIsGreaterThanPartialAmountRemaining = "The payment is greater than the partial amount remaining";
        public static string FinalPartialPaymentIsPaid = "Final partial payment received, invoice is now fully paid";
        public static string AnotherPartialPaymentReceived = "Another partial payment received, still not fully paid";
        public static string PaymentIsGreaterThanInvoiceAmount = "The payment is greater than the invoice amount";
        public static string InvoiceIsNowFullyPaid = "Invoice is now fully paid";
        public static string InvoiceIsNowPartiallyPaid = "Invoice is now partially paid";
    }
}
