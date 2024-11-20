using System.Security.Claims;
using VimaV2.DTOs;
using VimaV2.Repositories;
using VimaV2.Util;

namespace VimaV2.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly JwtTools _jwtTools;

        public AuthService(AuthRepository authRepository, JwtTools jwtTools)
        {
            _authRepository = authRepository;
            _jwtTools = jwtTools;
        }

        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Senha))
            {
                throw new ArgumentException("Email e senha são obrigatórios.");
            }

            // Busca o usuário pelo e-mail
            var usuario = await _authRepository.GetUsuarioByEmailAsync(loginDto.Email);

            // Verifica se o usuário existe e se a senha corresponde
            if (usuario == null || usuario.Senha != loginDto.Senha)
            {
                throw new UnauthorizedAccessException("Email ou senha incorretos.");
            }

            // Gera o token apenas para usuários válidos
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Role) // Adiciona a role do usuário
            });

            return _jwtTools.GerarToken(claims);
        }
    }
}

