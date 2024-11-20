using System.Security.Claims;

namespace VimaV2.Util
{
    public interface IJwtTokenService
    {
        string GenerateToken(ClaimsIdentity claimsIdentity, string role);
    }

}
