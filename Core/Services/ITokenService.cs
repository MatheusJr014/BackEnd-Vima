using System.Security.Claims;

namespace VimaV2.Infrastructure.Services

{
    public interface ITokenService
    {
        string GenerateToken(ClaimsIdentity claimsIdentity, string role);
        string GenerateToken(ClaimsIdentity claims);
        ClaimsPrincipal ValidateToken(string token);
    }
}
