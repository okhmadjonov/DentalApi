using Dental.Service.Interfaces.Students;
using Dental.Service.Interfaces.Tokens;
using Dental.Service.Interfaces.Users;
using Dental.Service.Services.Students;
using Dental.Service.Services.Users;
using Dental.Service.TokenGenerators;
using Microsoft.Extensions.DependencyInjection;

namespace Dental.Service.Extentions;

public static class AddExtensionServices
{
    public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthService>();
        services.AddScoped<ITokenRepository, TokenGenerator>();

        services.AddScoped<IUserRepository, UserService>();
        services.AddScoped<IStudentRepository, StudentService>();
        return services;
    }
}