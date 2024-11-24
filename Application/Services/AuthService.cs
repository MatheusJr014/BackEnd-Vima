using System.Security.Claims;
using VimaV2.DTOs;
using VimaV2.Repositories;
using VimaV2.Infrastructure.Services; // Namespace onde está a interface ITokenService

namespace VimaV2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService; // Interface para o TokenService

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Senha))
            {
                throw new ArgumentException("Email e senha são obrigatórios.");
            }

            var usuario = await _authRepository.GetUsuarioByEmailAsync(loginDto.Email);

            // Verifique se o usuário é nulo ou se a senha não corresponde
            if (usuario == null || string.IsNullOrEmpty(usuario.Senha) || usuario.Senha != loginDto.Senha)
            {
                throw new UnauthorizedAccessException("Email ou senha incorretos.");
            }

            if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Role))
            {
                throw new InvalidOperationException("Usuário inválido. Campos obrigatórios estão ausentes.");
            }

            var claims = new ClaimsIdentity(new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role)
             });

            return _tokenService.GenerateToken(claims);
        }


    }
}
