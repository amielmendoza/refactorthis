using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactorThis.Domain.Interfaces;
using RefactorThis.Persistence.Repositories;

namespace RefactorThis.Persistence
{
    public static class DependencyInjection
    {
        public async static Task<IServiceCollection> AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            return services;
        }
    }
}
