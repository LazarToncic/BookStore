using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Application.Common.Dto.Auth;
using BookStore.Application.Common.Interfaces;
using BookStore.Infrastructure.Configuration;
using BookStore.Infrastructure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Infrastructure.Services;

public class AuthService(ApplicationUserManager userManager, IOptions<JwtConfiguration> jwtOptions) : IAuthService
{

    private readonly JwtConfiguration _JwtConfiguration = jwtOptions.Value;
    private const string Purpose = "passwordless-auth";
    private const string Provider = "PasswordlessLoginTokenProvider";
    
    public async Task<BeginLoginResponseDto> BeginLoginAsync(string emailAddress)
    {
        var user = await userManager.FindByEmailAsync(emailAddress);
        string? validationToken = null;

        if (user == null)
            return new BeginLoginResponseDto(validationToken);

        var token = await userManager.GenerateUserTokenAsync(user, Provider, Purpose);
        var bytes = Encoding.UTF8.GetBytes($"{token}:{emailAddress}");
        validationToken = Convert.ToBase64String(bytes);
        
        //todo send an email with validation token
        return new BeginLoginResponseDto(validationToken);
    }

    public async Task<CompleteLoginResponseDto> CompleteLoginAsync(string validationToken)
    {
        var (userToken, emailAddress) = ExtractValidationToken(validationToken);
        var user = await userManager.FindByEmailAsync(emailAddress);

        if (user is not null)
        {
            var isValid = await userManager.VerifyUserTokenAsync(user, Provider, Purpose, userToken);

            if (!isValid)
                return new CompleteLoginResponseDto();

            await userManager.UpdateSecurityStampAsync(user);

            var authClaims = new List<Claim>();
            var roles = new List<string>();

            var rolesFromDb = await userManager.GetRolesAsync(user);

            foreach (var roleFromDb in rolesFromDb)
            {
                roles.Add(roleFromDb);
                authClaims.Add(new Claim(ClaimTypes.Role, roleFromDb));
            }

            return new CompleteLoginResponseDto(user.Email,
                roles,
                new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(authClaims)));
        }
        
        return new CompleteLoginResponseDto();
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtConfiguration.Secret!));

        var token = new JwtSecurityToken(
                issuer: _JwtConfiguration.ValidIssuer,
                audience: _JwtConfiguration.ValidAudience,
                expires: DateTime.Now.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    private static Tuple<string, string> ExtractValidationToken(string token)
    {
        var base64EncodesBytes = Convert.FromBase64String(token);
        var tokenDetails = Encoding.UTF8.GetString(base64EncodesBytes);
        var separatorIndex = tokenDetails.IndexOf(':');

        return new Tuple<string, string>(tokenDetails[..separatorIndex], tokenDetails[(separatorIndex + 1)..]);
    }
}