using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VimaV2.Models;  // Ajuste o namespace para o modelo de User
using VimaV2.Database;
using VimaV2.Util;
using VimaV2.DTOs;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly VimaV2DbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(VimaV2DbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Senha))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            var usuarioEncontrado = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (usuarioEncontrado == null || usuarioEncontrado.Senha != login.Senha)
            {
                return BadRequest("Email ou senha incorretos.");
            }

            var token = JwtTools.GerarToken(usuarioEncontrado, _configuration);

            return Ok(new { token });
        }
    }
}
