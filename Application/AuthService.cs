using System;
using System.Security.Claims;
using System.Threading.Tasks;
using VimaV2.DTOs;
using VimaV2.Repositories;
using VimaV2.Util;

namespace VimaV2.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly IJwtTokenService _jwtTokenService;

        // Alteração: Injeção de dependência para IJwtTokenService em vez de JwtTools
        public AuthService(AuthRepository authRepository, IJwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Senha))
            {
                throw new ArgumentException("Email e senha são obrigatórios.");
            }

            var usuario = await _authRepository.GetUsuarioByEmailAsync(loginDto.Email);

            if (usuario == null || usuario.Senha != loginDto.Senha)
            {
                throw new UnauthorizedAccessException("Email ou senha incorretos.");
            }

            var claims = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            });

            // Passe o role do usuário ao gerar o token
            return _jwtTokenService.GenerateToken(claims, usuario.Role);
        }

    }
}
