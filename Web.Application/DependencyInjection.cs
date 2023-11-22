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
            services.AddTransient<IAvatarService, AvatarService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IAuthorizedUserService, AuthorizedUserService>();
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