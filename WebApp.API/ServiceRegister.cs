using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Repositories;
using Aow.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace WebApp.API
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(
               this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }
          
            @this.AddTransient<IProductRepository, ProductRepository>();
            @this.AddTransient<ICompanyRepository, CompanyRepository>();
            @this.AddTransient<IFinancialYearRepository, FinancialYearRepository>();
            @this.AddTransient<IUserCompanyRepository, UserCompanyRepository>();
            @this.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            return @this;
        }
    }
}
