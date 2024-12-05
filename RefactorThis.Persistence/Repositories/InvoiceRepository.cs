using RefactorThis.Domain.Interfaces;

namespace RefactorThis.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private Invoice _invoice;
        public Invoice? GetInvoiceByReference(string reference)
        {
            return _invoice;
        }

        public void Add(Invoice invoice)
        {
            _invoice = invoice;
        }

        public void Save(Invoice entity)
        {
        }
    }
}