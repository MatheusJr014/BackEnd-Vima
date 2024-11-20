using Microsoft.AspNetCore.Mvc;
using VimaV2.Application;
using VimaV2.Database;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Services; // Ajuste o namespace para a classe User

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly AdminService _adminService;

        public UsuariosController(UsuarioService usuarioService, AdminService adminService)
        {
            _usuarioService = usuarioService;

            _adminService = adminService; 
        }



        // GET: api/usuarios
        [HttpGet]

        public ActionResult<List<UsuarioDTO>> GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            return Ok(usuarios);
        }


        // POST: api/Usuario
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUsuario([FromBody] UsuarioDTO usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("Dados do usuário são obrigatórios.");
            }

            // Verifique se o modelo de dados do usuário está correto
            if (string.IsNullOrEmpty(usuarioDto.Nome) || string.IsNullOrEmpty(usuarioDto.Email))
            {
                return BadRequest("Nome e Email são obrigatórios.");
            }

            var usuarioCriado = await _usuarioService.AddUsuarioAsync(usuarioDto);
            return Ok(new { Message = "Usuário criado com sucesso.", Usuario = usuarioCriado });
        }


        //GET POR ID
        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var user = _usuarioService.GetUsuarioById(id); // Chama o serviço

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            // Define o Role como 'Admin'
            usuario.Role = "Admin";
            var resultado = await _adminService.RegisterAdminAsync(usuario);

            if (resultado == null)
            {
                return BadRequest("Erro ao cadastrar administrador.");
            }

            return Ok(new { Message = "Administrador cadastrado com sucesso.", Usuario = resultado });
        }



    }
}
