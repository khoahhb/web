using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Domain.Context;
using Web.Infracturre.DbFactories;
using Web.Infracturre.Repositories.AvatarRepo;
using Web.Infracturre.Repositories.BaseRepo;
using Web.Infracturre.Repositories.UserProfileRepo;
using Web.Infracturre.Repositories.UserRepo;
using Web.Infracturre.UnitOfWorks;

namespace Web.Infracturre
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Web.Domain")));

            return services;
        }

        public static IServiceCollection AddInfrastuctures(this IServiceCollection services)
        {
            services.AddScoped<Func<ApplicationDbContext>>((provider) => () => provider.GetService<ApplicationDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAvatarRepository, AvataRepository>();

            return services;
        }
    }
}
