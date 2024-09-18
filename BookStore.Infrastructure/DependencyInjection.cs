using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Auth.Extensions;
using BookStore.Infrastructure.Configuration;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Identity;
using BookStore.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfiguration = new PostgresDbConfiguration();
        configuration.GetSection("PostgresDbConfiguration").Bind(dbConfiguration);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Test")
        {
            services.AddDbContext<DemoDbContext>(options =>
                options.UseNpgsql(dbConfiguration.ConnectionString(),
                    x => x.MigrationsAssembly(typeof(DemoDbContext).Assembly.FullName)));  
        }

        /*services.AddDbContext<DemoDbContext>(options =>
            options.UseNpgsql(dbConfiguration.ConnectionString(),
                x => x.MigrationsAssembly(typeof(DemoDbContext).Assembly.FullName)));*/

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddUserManager<ApplicationUserManager>()
            .AddEntityFrameworkStores<DemoDbContext>()
            .AddDefaultTokenProviders()
            .AddPasswordlessLoginTokenProvider();

        services.AddScoped<IDemoDbContext>(provider => provider.GetRequiredService<DemoDbContext>());
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleServices>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));

        return services;
    }
}