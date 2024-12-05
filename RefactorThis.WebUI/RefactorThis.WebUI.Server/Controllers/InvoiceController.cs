using Microsoft.AspNetCore.Mvc;
using RefactorThis.Domain.Interfaces;
using RefactorThis.Persistence;

namespace RefactorThis.WebUI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private IInvoiceService _invoiceService;

        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public ActionResult PostPayment([FromBody]Payment payment)
        {
            Invoice invoiceSample = new Invoice() { Amount = 100, AmountPaid = 0, Payments = new List<Payment>() };

            string paymentResult = _invoiceService.ProcessPayment(payment, invoiceSample);

            return Ok(paymentResult);
        }
    }
}
