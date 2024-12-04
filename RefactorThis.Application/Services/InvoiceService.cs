using RefactorThis.Application.Processors.PaymentTypes;
using RefactorThis.Domain.Common.Constants;
using RefactorThis.Domain.Common.Enums;
using RefactorThis.Domain.Exceptions;
using RefactorThis.Domain.Interfaces;
using RefactorThis.Persistence;

namespace RefactorThis.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;

        public InvoiceService(IInvoiceRepository invoiceRepository, IPaymentProcessorFactory paymentProcessorFactory)
        {
            _invoiceRepository = invoiceRepository;
            _paymentProcessorFactory = paymentProcessorFactory;
        }

        public string ProcessPayment(Payment payment, Invoice? invoiceSample = null)
        {
            Invoice invoice = invoiceSample ?? _invoiceRepository.GetInvoiceByReference(payment.Reference);

            if (invoice == null)
            {
                throw new InvoiceNotFoundException("There is no invoice matching this payment");
            }

            if (invoice.Amount == 0)
            {
                if (invoice.Payments == null || !invoice.Payments.Any())
                    return InvoiceResponseMessages.NoPaymentNeeded;
                else
                    throw new InvalidInvoiceStateException("The invoice is in an invalid state, it has an amount of 0 and it has payments.");
            }

            IPaymentProcessor paymentProcessor = GetPaymentProcessor(invoice);
            string responseMessage = paymentProcessor.ProcessPayment(invoice, payment);

            _invoiceRepository.Save(invoice);
            return responseMessage;
        }

        private IPaymentProcessor GetPaymentProcessor(Invoice invoice)
        {
            if (invoice.Payments != null && invoice.Payments.Any())
            {
                return _paymentProcessorFactory.GetPaymentProcessor(PaymentType.Partial);
            }
            return _paymentProcessorFactory.GetPaymentProcessor(PaymentType.Full);
        }
    }

    
}