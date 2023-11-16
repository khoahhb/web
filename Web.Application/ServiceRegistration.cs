using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Web.Application.Helpers;
using Web.Application.Interfaces;
using Web.Application.Services;
using Web.Application.Validation.Avatar;
using Web.Application.Validation.User;
using Web.Application.Validation.UserProfile;

namespace Web.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IAvatarService, AvatarService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();

            // Add validators
            services.AddValidatorsFromAssemblyContaining<CreateAvatarValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAvatarValidator>();

            services.AddValidatorsFromAssemblyContaining<SignInValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateUserProfileValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserProfileValidator>();

            // Add auto mapper profile
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }
    }
}