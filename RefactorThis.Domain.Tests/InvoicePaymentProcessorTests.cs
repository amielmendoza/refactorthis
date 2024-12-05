using NUnit.Framework;
using RefactorThis.Application.Processors.PaymentTypes;
using RefactorThis.Application.Services;
using RefactorThis.Domain.Common.Constants;
using RefactorThis.Domain.Exceptions;
using RefactorThis.Persistence;
using RefactorThis.Persistence.Repositories;

namespace RefactorThis.Domain.Tests
{
    [TestFixture]
    public class InvoicePaymentProcessorTests
    {
        private InvoiceRepository _invoiceRepository;
        private InvoiceService _invoiceService;
        private PaymentProcessorFactory _paymentProcessorFactory;

        [SetUp]
        public void Setup()
        {
            _invoiceRepository = new InvoiceRepository();
            _paymentProcessorFactory = new PaymentProcessorFactory();
        }
        [Test]
        public void ProcessPayment_Should_ThrowException_When_NoInoiceFoundForPaymentReference()
        {
            var payment = new Payment();
            var failureMessage = "";

            try
            {
                _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);
                var result = _invoiceService.ProcessPayment(payment);
            }
            catch (InvoiceNotFoundException e)
            {
                failureMessage = e.Message;
            }

            Assert.AreEqual("There is no invoice matching this payment", failureMessage);
        }

        [Test]
        public void ProcessPayment_Should_ReturnFailureMessage_When_NoPaymentNeeded()
        {
            var invoice = new Invoice()
            {
                Amount = 0,
                AmountPaid = 0,
                Payments = null
            };

            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment();

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.NoPaymentNeeded, result);
        }

        [Test]
        public void ProcessPayment_Should_ReturnFailureMessage_When_InvoiceAlreadyFullyPaid()
        {
            var invoice = new Invoice()
            {
                Amount = 10,
                AmountPaid = 10,
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Amount = 10
                    }
                }
            };
            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment();

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.FullyPaid, result);
        }

        [Test]
        public void ProcessPayment_Should_ReturnFailureMessage_When_PartialPaymentExistsAndAmountPaidExceedsAmountDue()
        {
            var invoice = new Invoice()
            {
                Amount = 10,
                AmountPaid = 5,
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Amount = 5
                    }
                }
            };
            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment()
            {
                Amount = 6
            };

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.PaymentIsGreaterThanPartialAmountRemaining, result);
        }

        [Test]
        public void ProcessPayment_Should_ReturnFailureMessage_When_NoPartialPaymentExistsAndAmountPaidExceedsInvoiceAmount()
        {
            var invoice = new Invoice()
            {
                Amount = 5,
                AmountPaid = 0,
                Payments = new List<Payment>()
            };
            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment()
            {
                Amount = 6
            };

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.PaymentIsGreaterThanInvoiceAmount, result);
        }

        [Test]
        public void ProcessPayment_Should_ReturnFullyPaidMessage_When_PartialPaymentExistsAndAmountPaidAreEqualAmountDue()
        {
            var invoice = new Invoice()
            {
                Amount = 10,
                AmountPaid = 5,
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Amount = 5
                    }
                }
            };
            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment()
            {
                Amount = 5
            };

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.FinalPartialPaymentIsPaid, result);
        }

        [Test]
        public void ProcessPayment_Should_ReturnFullyPaidMessage_When_NoPartialPaymentExistsAndAmountPaidAreEqualInvoiceAmount()
        {
            var invoice = new Invoice()
            {
                Amount = 10,
                AmountPaid = 0,
                Payments = new List<Payment>() { new Payment() { Amount = 10 } }
            };
            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment()
            {
                Amount = 10
            };

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.FullyPaid, result);
        }

        [Test]
        public void ProcessPayment_Should_ReturnPartiallyPaidMessage_When_PartialPaymentExistsAndAmountPaidIsLessThanAmountDue()
        {
            var invoice = new Invoice()
            {
                Amount = 10,
                AmountPaid = 5,
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Amount = 5
                    }
                }
            };
            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment()
            {
                Amount = 1
            };

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.AnotherPartialPaymentReceived, result);
        }

        [Test]
        public void ProcessPayment_Should_ReturnPartiallyPaidMessage_When_NoPartialPaymentExistsAndAmountPaidIsLessThanInvoiceAmount()
        {
            var invoice = new Invoice()
            {
                Amount = 10,
                AmountPaid = 0,
                Payments = new List<Payment>()
            };
            _invoiceRepository.Add(invoice);

            _invoiceService = new InvoiceService(_invoiceRepository, _paymentProcessorFactory);

            var payment = new Payment()
            {
                Amount = 1
            };

            var result = _invoiceService.ProcessPayment(payment);

            Assert.AreEqual(InvoiceResponseMessages.InvoiceIsNowPartiallyPaid, result);
        }
    }
}