using FCoin.Business.Interfaces;
using FCoin.Business;
using FCoin.Repositories.Interfaces;
using FCoin.Repositories;

namespace FCoin.Configuration
{
    public static class AddServiceScopes
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IClientManagement, ClientManagement>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IHourManagement, HourManagement>();

            services.AddScoped<ISelectorManagement, SelectorManagement>();
            services.AddScoped<ISelectorRepository, SelectorRepository>();

            services.AddScoped<IValidatorManagement, ValidatorManagement>();
            services.AddScoped<IValidatorRepository, ValidatorRepository>();

            services.AddScoped<ITransactionManagement, TransactionManagement>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<ITransactionLinkRepository, TransactionLinkRepository>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

    }
}
