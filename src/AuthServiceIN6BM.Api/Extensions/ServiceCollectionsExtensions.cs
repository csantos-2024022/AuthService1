using AuthServiceIN6BM.Domain.Interfaces;
using AuthServiceIN6BM.Application.Interfaces;
using AuthServiceIN6BM.Persistence.Data;
using AuthServiceIN6BM.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceIN6BM.Api.Extensions;

public static class ServiceCollectionExtensios
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UserNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UserSnakeCaseNamingConventions());
                


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddHealtChecks();

        return services;
            
    }

    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddEndPointApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    
}