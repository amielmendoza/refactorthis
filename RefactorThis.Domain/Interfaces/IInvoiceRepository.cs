using RefactorThis.Persistence;

namespace RefactorThis.Domain.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        public Invoice GetInvoiceByReference(string reference);
    }
}
