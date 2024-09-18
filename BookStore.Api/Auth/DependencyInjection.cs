using BookStore.Api.Auth.Constants;
using BookStore.Api.Auth.Options;
using BookStore.Api.Auth.Schemes;

namespace BookStore.Api.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddNsiBookStoreAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddScheme<HeaderBasicAuthenticationSchemeOptions, HeaderBasicAuthenticationSchemeHandler>(
                AuthConstants.HeaderBasicAuthenticationScheme,
                schemeOptions => configuration.GetSection("Auth:Header")
                    .Bind(schemeOptions));

        return services;

    }
}