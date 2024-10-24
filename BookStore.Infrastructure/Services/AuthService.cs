using BookStore.Application.Common.Dto.Auth;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Exceptions;
using BookStore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Infrastructure.Services;

public class AuthService(ApplicationUserManager userManager, IPasswordHasher<ApplicationUser> _passwordHasher, SignInManager<ApplicationUser> signInManager) : IAuthService
{

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingEmail = await userManager.FindByEmailAsync(dto.Email);

        if (existingEmail != null)
            throw new AuthException("User with this email already exists.");

        var existingUsername = await userManager.FindByNameAsync(dto.Username);
        
        if (existingUsername != null)
            throw new AuthException("User with this username already exists.");
        
        var user = new ApplicationUser
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Username,
            PhoneNumber = dto.PhoneNumber
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        try
        {
            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new AuthException("Could not create new user",
                    new {Errors = result.Errors.ToList()});
            }

            var rolesResult = await userManager.AddToRoleAsync(user, "Customer");

            if (!rolesResult.Succeeded)
            {
                await userManager.DeleteAsync(user);

                throw new AuthException("Could not add roles to the user",
                    new {Errors = rolesResult.Errors.ToList()});
            }
        }
        catch (Exception e)
        {
            await userManager.DeleteAsync(user);
            throw new AuthException("Could not add roles to the user",
                e);
        }
        
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.EmailOrUsername);
        
        if (user == null)
        {
            user = await userManager.FindByNameAsync(dto.EmailOrUsername);
        }
        
        if (user == null)
        {
            return new LoginResponseDto(false, false);
        }
        
        var result = await signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, lockoutOnFailure: false);

        return new LoginResponseDto(result.Succeeded, result.IsLockedOut);
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}