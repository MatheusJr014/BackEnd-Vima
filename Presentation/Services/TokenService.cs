using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VimaV2.Infrastructure.Services; // Referência à interface no Core

namespace VimaV2.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly string _secretKey;


        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

            _secretKey = configuration["Jwt:SECRET_KEY"]
                        ?? throw new InvalidOperationException("Jwt:SECRET_KEY não está configurada.");

        }

        // Método para gerar o token com base nas claims passadas
        public string GenerateToken(ClaimsIdentity claimsIdentity, string role)
        {
            // Adicionando a claim 'role' ao ClaimsIdentity
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

            var tokenHandler = new JwtSecurityTokenHandler();

            // Lê a chave de configuração
            var key = _configuration[_secretKey];

            // Verifica se a chave SECRET_KEY está configurada corretamente
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Jwt:SECRET_KEY não foi configurada corretamente.");
            }

            var keyBytes = Encoding.UTF8.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(1), // Defina o tempo de expiração do token conforme necessário
                Issuer = _configuration["Jwt:Issuer"], // Lê o Issuer do appsettings.json
                Audience = _configuration["Jwt:Audience"], // Lê o Audience do appsettings.json
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Método para gerar o token sem adicionar a role
        public string GenerateToken(ClaimsIdentity claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Lê a chave de configuração
            var key = _configuration[_secretKey];

            // Verifica se a chave SECRET_KEY está configurada corretamente
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Jwt:SECRET_KEY não foi configurada corretamente.");
            }

            var keyBytes = Encoding.UTF8.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1), // Defina o tempo de expiração do token conforme necessário
                Issuer = _configuration["Jwt:Issuer"], // Lê o Issuer do appsettings.json
                Audience = _configuration["Jwt:Audience"], // Lê o Audience do appsettings.json
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Método para validar o token
        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Lê a chave de configuração
            var key = _configuration[_secretKey];

            // Verifica se a chave SECRET_KEY está configurada corretamente
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Jwt:SECRET_KEY não foi configurada corretamente.");
            }

            var keyBytes = Encoding.ASCII.GetBytes(key);

            try
            {
                return tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"], // Lê o Issuer do appsettings.json
                    ValidAudience = _configuration["Jwt:Audience"], // Lê o Audience do appsettings.json
                    ClockSkew = TimeSpan.Zero
                }, out _);
            }
            catch
            {
                return null;
            }
        }
    }
}