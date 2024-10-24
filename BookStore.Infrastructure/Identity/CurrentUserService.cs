using System.Security.Authentication;
using System.Security.Claims;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Constants;
using Microsoft.AspNetCore.Http;

namespace BookStore.Infrastructure.Identity;

public class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
{
    public string GetCurrentUser()
    {
        if (contextAccessor.HttpContext == null)
        {
            throw new AuthenticationException("You need to be logged in");
        }

        var loggedInUser = contextAccessor.HttpContext.User;
        var requestingUserId = loggedInUser.FindFirstValue(ClaimTypes.NameIdentifier);

        if (requestingUserId == null)
            throw new Exception("You need to be logged in");

        return requestingUserId;
    }
}