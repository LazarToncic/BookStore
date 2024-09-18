using System.Security.Claims;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Constants;
using Microsoft.AspNetCore.Http;

namespace BookStore.Infrastructure.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public string? UserId { get; private set; }
    public string? FirstName { get; private set;}
    public string? LastName { get; private set;}
    public string? Email { get; private set;}
    public List<string>? Roles { get; private set; } = new();
    public bool IsOwner { get; private set;}

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        
        UserId = GetClaimValue(ClaimTypes.NameIdentifier);

        var identity = httpContextAccessor.HttpContext?.User.Identity;

        if (identity is not null && identity.IsAuthenticated)
        {
            var roles = GetRoleClaimValues();

            if (roles?.Count > 0)
            {
                Roles.AddRange(roles);
            }

            Email = GetClaimValue(ClaimTypes.Email);
            FirstName = GetClaimValue(ClaimTypes.Email);
            LastName = GetClaimValue(ClaimTypes.Email);

            IsOwner = Roles.Contains(AuthorizationConstants.Owner);
        }
    }

    private string? GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(claimType)?.Value;
    }

    private List<string>? GetRoleClaimValues()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value)
            .ToList();
    }
}