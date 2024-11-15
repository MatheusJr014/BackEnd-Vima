using Microsoft.AspNetCore.Mvc;
using VimaV2.Application;
using VimaV2.Database;
using VimaV2.DTOs;
using VimaV2.Models; // Ajuste o namespace para a classe User

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;


        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService; 
        }



        // GET: api/usuarios
        [HttpGet]

        public ActionResult<List<UsuarioDTO>> GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            return Ok(usuarios);
        }


        // POST: api/Usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioDTO usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("Usuario não pode ser nulo.");
            }

            var usuarioCriado = await _usuarioService.AddUsuarioAsync(usuarioDto);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioCriado.Id }, usuarioCriado);
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
    }
}
