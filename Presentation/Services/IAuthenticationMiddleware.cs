using Microsoft.AspNetCore.Http;

namespace VimaV2.Middleware
{
    public interface IAuthenticationMiddleware
    {
        // Método para processar a requisição e validar o token
        Task InvokeAsync(HttpContext context);
    }
}
