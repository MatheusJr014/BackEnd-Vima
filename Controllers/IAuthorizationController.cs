using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VimaV2.DTOs;

namespace VimaV2.Controllers
{
    public interface IAuthorizationController
    {
        // Método para garantir que o usuário tenha permissão para acessar uma ação específica
        [Authorize(Roles = "Admin")]
        Task<IActionResult> CreateProduct([FromBody] ProdutoDTO produto);
    }
}
