using Microsoft.AspNetCore.Mvc;
using VimaV2.Database;
using VimaV2.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VimaV2.Services;
using VimaV2.DTOs;

namespace VimaV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly ContatoService _contatoService;

        public ContatosController(ContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        // GET: /api/contato
        [HttpGet]

        public ActionResult<List<ContatoDTO>> GetAll()
        {
            var contatos = _contatoService.getAll();
            return Ok(contatos);
        }

        //POST: /Api/Criar

        [HttpPost("save")]
        public async Task<IActionResult> CreateContato([FromBody] ContatoDTO contatoDTO)
        {
            if (contatoDTO == null)
            {
                return BadRequest("Contato não pode ser nulo.");
            }

            var contatoCriado = await _contatoService.CreateContatoAsync(contatoDTO);

            return Ok(contatoCriado); // Simplesmente retorna o objeto criado
        }
    }
}
