using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using VimaV2.Infrastructure.Services; // Certifique-se de usar a namespace correta para ITokenService

namespace VimaV2.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService; // Usando a interface ITokenService

        public JwtAuthenticationMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService; // Injeção de dependência da interface ITokenService
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Obtém o token de autorização do cabeçalho
            var token = context.Request.Headers["Authorization"].ToString()?.Split(" ").Last();

            if (token != null)
            {
                // Valida o token com o ITokenService
                var principal = _tokenService.ValidateToken(token);
                if (principal != null)
                {
                    // Se o token for válido, define o usuário no contexto da requisição
                    context.User = principal;
                }
            }

            // Chama o próximo middleware
            await _next(context);
        }
    }
}
