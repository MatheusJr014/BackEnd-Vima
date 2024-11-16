using Microsoft.AspNetCore.Mvc;
using VimaV2.Services;
using VimaV2.DTOs;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                // Chama o serviço de autenticação
                var token = await _authService.LoginAsync(login);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                // Retorna erro de autenticação
                return Unauthorized("Email ou senha incorretos.");
            }
            catch (Exception ex)
            {
                // Retorna erro genérico
                return BadRequest(ex.Message);
            }
        }

    }
}
