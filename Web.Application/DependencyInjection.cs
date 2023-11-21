using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Web.Application.Interfaces;
using Web.Application.Services;

namespace Web.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAvatarService, AvatarService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            return services;
        }

        public static IServiceCollection AddValidatorsFromAppServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
        public static IServiceCollection AddAutoMapperFromAppServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}