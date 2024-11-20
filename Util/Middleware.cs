using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using VimaV2.Util; // Assegure-se de que JwtTokenService esteja no namespace correto

namespace VimaV2.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        // Injeta o JwtTokenService através do construtor
        private readonly JwtTokenService _jwtTokenService;

        public JwtAuthenticationMiddleware(RequestDelegate next, JwtTokenService jwtTokenService)
        {
            _next = next;
            _jwtTokenService = jwtTokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Obtém o token de autorização do cabeçalho
            var token = context.Request.Headers["Authorization"].ToString()?.Split(" ").Last();

            if (token != null)
            {
                // Valida o token com o JwtTokenService
                var principal = _jwtTokenService.ValidateToken(token);
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
