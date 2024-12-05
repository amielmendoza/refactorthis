using RefactorThis.Domain.Common.Enums;

namespace RefactorThis.Application.Processors.PaymentTypes
{
    public interface IPaymentProcessorFactory
    {
        IPaymentProcessor GetPaymentProcessor(PaymentType isFullPayment);
    }

    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor GetPaymentProcessor(PaymentType paymentType)
        {
            return paymentType switch
            {
                PaymentType.Full => new FullPaymentProcessor(),
                PaymentType.Partial => new PartialPaymentProcessor(),
                _ => throw new ArgumentOutOfRangeException(nameof(paymentType), paymentType, null)
            };
        }
    }
}
