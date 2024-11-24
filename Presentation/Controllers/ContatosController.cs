using Microsoft.AspNetCore.Mvc;
using VimaV2.DTOs;
using VimaV2.Services;
using System.Threading.Tasks;
using VimaV2.Models;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatosController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        // GET: /api/contato
        [HttpGet]
        public async Task<ActionResult<List<ContatoDTO>>> GetAll()
        {
            var contatos = await _contatoService.GetAllContactsAsync();
            return Ok(contatos); // Retorna os contatos obtidos
        }

        // POST: /api/contato/save
        [HttpPost("save")]
        public async Task<IActionResult> CreateContato([FromBody] ContatoDTO contatoDTO)
        {
            if (contatoDTO == null)
            {
                return BadRequest("Contato não pode ser nulo.");
            }

            // Converte o ContatoDTO para Contato (modelo de dados)
            var contato = new Contato
            {
                Name = contatoDTO.Name,
                Sobrenome = contatoDTO.Sobrenome,
                Email = contatoDTO.Email,
                Assunto = contatoDTO.Assunto,
                Description = contatoDTO.Description
            };

            var contatoCriado = await _contatoService.AddContactAsync(contato);

            return Ok(contatoCriado); // Retorna o contato criado
        }
    }
}
