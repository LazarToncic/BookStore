using BookStore.Infrastructure.Configuration;
using BookStore.Infrastructure.Context;
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
        
        services.AddDbContext<DemoDbContext>(options =>
            options.UseNpgsql(dbConfiguration.ConnectionString(),
                x => x.MigrationsAssembly(typeof(DemoDbContext).Assembly.FullName)));  

        return services;
    }
}