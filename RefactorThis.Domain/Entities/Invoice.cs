using RefactorThis.Domain.Common;
using RefactorThis.Domain.Common.Enums;

namespace RefactorThis.Persistence
{
    public class Invoice : AuditableEntity
    {
        public decimal Amount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal TaxAmount { get; set; }
        public List<Payment> Payments { get; set; }
        public InvoiceType Type { get; set; }
    }
}