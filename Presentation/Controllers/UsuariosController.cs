using Microsoft.AspNetCore.Mvc;
using VimaV2.Application;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;  // Usar interface para o serviço
        private readonly IAdminService _adminService;      // Usar interface para o serviço

        // Injeção de dependência
        public UsuariosController(IUsuarioService usuarioService, IAdminService adminService)
        {
            _usuarioService = usuarioService;
            _adminService = adminService;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync(); // Chama o método assíncrono
            return Ok(usuarios);
        }

        // POST: api/Usuario/register
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

        // GET por ID: api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var user = await _usuarioService.GetUsuarioByIdAsync(id); // Chama o método assíncrono

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UsuarioDTO usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            // Verifique se a senha e o sobrenome foram fornecidos
            if (string.IsNullOrEmpty(usuarioDto.Senha))
            {
                return BadRequest("A senha não pode ser vazia.");
            }

            if (string.IsNullOrEmpty(usuarioDto.Sobrenome))
            {
                return BadRequest("O sobrenome não pode ser vazio.");
            }

            // Converte o DTO para a entidade Usuario
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Sobrenome = usuarioDto.Sobrenome,
                Email = usuarioDto.Email,
                Role = "Admin",  // Definir como Admin
                Senha = usuarioDto.Senha,  // Senha do DTO
            };

       

            var resultado = await _adminService.AddUsuarioAsync(usuario);

            if (resultado == null)
            {
                return BadRequest("Erro ao cadastrar administrador.");
            }

            return Ok(new { Message = "Administrador cadastrado com sucesso.", Usuario = resultado });
        }

    }
}
