using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactorThis.Application.Processors.PaymentTypes;
using RefactorThis.Application.Services;
using RefactorThis.Domain.Interfaces;

namespace RefactorThis.Application
{
    public static class DependencyInjection
    {
        public async static Task<IServiceCollection> AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();
            return services;
        }
    }
}
