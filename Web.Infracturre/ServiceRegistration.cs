using Microsoft.Extensions.DependencyInjection;
using Web.Infracturre.Interfaces;

namespace Web.Infracturre
{
    public static class ServiceRegistration
    {
        public static void AddInfractureLayer(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
