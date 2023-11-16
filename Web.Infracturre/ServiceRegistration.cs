using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Domain.Context;
using Web.Infracturre.Interfaces;
using Web.Infracturre.Repositories;

namespace Web.Infracturre
{
    public static class ServiceRegistration
    {
        public static void AddInfractureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Web.Domain")));

            services.AddScoped<Func<ApplicationDbContext>>((provider) => () => provider.GetService<ApplicationDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAvatarRepository, AvataRepository>();
        }
    }
}
